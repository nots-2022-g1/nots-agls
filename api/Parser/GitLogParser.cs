using System.Text.RegularExpressions;
using api.Model;
using Serilog;

namespace api.Parser;

public class GitLogParser
{
    private readonly IConfiguration _config;
    private readonly string _directory;

    public GitLogParser(IConfiguration config)
    {
        _config = config;
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
    public static Task<IEnumerable<GitCommit>> Parse(GitRepo repo, string input)
    {
        var commitStart = new Regex("^commit\\s", RegexOptions.Multiline | RegexOptions.Compiled);
        IEnumerable<string> commits = commitStart.Split(input);

        ICollection<GitCommit> gitCommits = commits
            .Where(c => !c.Equals(string.Empty))
            .Where(c => !c.Contains("Merge", StringComparison.CurrentCultureIgnoreCase))
            .Select(commit => new GitCommit
            {
                Hash = ParseHash(commit),
                Message = ParseMessage(commit),
                GitRepoId = repo.Id
            }).ToList();

        return Task.FromResult<IEnumerable<GitCommit>>(gitCommits);
    }

    private static string ParseHash(string input)
    {
        var splitBySpace = input.Split('\n');
        return splitBySpace.First();
        // var commitHash = new Regex("^commit ([0-9a-f]{40})", RegexOptions.Compiled | RegexOptions.Multiline);
        // return commitHash.Match(input).Groups[1].Value;
    }

    private static string ParseMessage(string input)
    {
        var splitByDoubleNewline = input.Split("\n\n");
        // var messageRegex = new Regex("\n{2}", RegexOptions.Compiled | RegexOptions.Multiline);
        // return messageRegex.Split(input)[1];
        return splitByDoubleNewline[1].TrimStart();
    }
}