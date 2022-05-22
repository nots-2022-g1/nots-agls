using System.Text.RegularExpressions;
using api.Models;
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

    public static async IAsyncEnumerable<GitCommit> ParseGitCommitsAsync(string input, GitRepo repo)
    {
        var commitStart = new Regex("^commit\\s", RegexOptions.Multiline | RegexOptions.Compiled);
        IEnumerable<string> commits = commitStart.Split(input);
        
        foreach (var commit in commits)
        {
            if (commit.Equals(string.Empty)) continue;
            if (commit.Contains("Merge", StringComparison.CurrentCultureIgnoreCase)) continue;
            var gitCommit = new GitCommit
            {
                Hash = await ParseHash(commit),
                Message = await ParseMessage(commit),
                GitRepoId = repo.Id
            };
            yield return gitCommit;
        }
    }

    private static Task<string> ParseHash(string input)
    {
        return Task.Run(() => input.Split('\n').First());
    }

    private static Task<string> ParseMessage(string input)
    {
        return Task.Run(() => input.Split("\n\n")[1].TrimStart());
    }
}