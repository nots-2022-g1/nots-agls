using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using api.Model;
using api.Services;
using Serilog;

namespace api.Parser;

public class GitLogParser
{
    private readonly IConfiguration _config;
    private readonly IGitService _gitService;
    private readonly string _directory;

    public GitLogParser(IConfiguration config, IGitService gitService)
    {
        _config = config;
        _gitService = gitService;
        _directory = Path.Combine(Path.GetTempPath(), "gitlogparser");
    }

    private async Task<int> Count()
    {
        using var gitCount = new GitProcess(_directory, "rev-list HEAD --count");
        gitCount.Start();
        await gitCount.WaitForExitAsync();
        int count;
        try
        {
            count = int.Parse((await gitCount.StandardOutput.ReadLineAsync())!);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error parsing commit count");
            throw;
        }

        return count;
    }

    /*
     * This method takes a string containing the raw output of 'git log' and a GitRepo object.
     * It parses the input string to GitCommit objects, and uses the GitRepo object to determine
     * the GitCommit.GitRepoId property. It uses a queue<Task> and Task.WhenAll to enable the workload
     * to be spread to multiple threads.
     */
    public async Task<IEnumerable<GitCommit>> Parse(GitRepo repo, string input)
    {
        var commitStart = new Regex("^commit\\s[0-9a-f]{40}", RegexOptions.Compiled & RegexOptions.Multiline);
        IEnumerable<string> commits = commitStart.Split(input);
        var threadSafeGitCommits = new ConcurrentBag<GitCommit>();
        var tasks = new Queue<Task>();

        foreach (var commit in commits)
        {
            tasks.Enqueue(Task.Run(() =>
                {
                    threadSafeGitCommits.Add(new GitCommit
                        {
                            Hash = ParseHash(commit),
                            Message = ParseMessage(input),
                            GitRepoId = repo.Id
                        }
                    );
                }
            ));
        }

        await Task.WhenAll(tasks);

        return threadSafeGitCommits;
    }

    private string ParseHash(string input)
    {
        var commitHash = new Regex("^commit ([0-9a-f]{40})", RegexOptions.Compiled & RegexOptions.Multiline);
        return commitHash.Match(input).Groups[1].Value;
    }

    private string ParseMessage(string input)
    {
        var messageRegex = new Regex("\n{2}", RegexOptions.Compiled & RegexOptions.Multiline);
        return messageRegex.Split(input)[1];
    }

   
}