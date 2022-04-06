using System.Diagnostics;
using System.Text.RegularExpressions;
using Serilog;

namespace api.Parser;

public class GitLogParser
{
    private readonly string _directory;

    public GitLogParser()
    {
        _directory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
    }

    public async Task<List<Commit>> Parse(Uri location)
    {
        try
        {
            Directory.CreateDirectory(_directory);
            var repoName = location.Segments.Last();

            using var git = new Process();
            git.StartInfo.UseShellExecute = false;
            git.StartInfo.FileName = "git";
            git.StartInfo.CreateNoWindow = true;
            git.StartInfo.WorkingDirectory = _directory;

            using var gitClone = git;
            git.StartInfo.Arguments = $"clone {location.AbsoluteUri}";
            gitClone.Start();
            await gitClone.WaitForExitAsync();

            using var gitLog = git;
            gitLog.StartInfo.WorkingDirectory = Path.Combine(_directory, repoName);
            git.StartInfo.Arguments = "log --date=iso";
            git.StartInfo.RedirectStandardOutput = true;
            gitLog.Start();
            var gitlog = gitLog.WaitForExitAsync();
            var pCommits = new List<Commit>();
            Commit? pCommit = null;
            while (!git.StandardOutput.EndOfStream)
            {
                var line = await git.StandardOutput.ReadLineAsync() ?? string.Empty;
                if (line.Equals(string.Empty)) continue;
                if (Regex.IsMatch(line, "^commit\\s[0-9a-f]{40}"))
                {
                    //now we know a new commit starts
                    //read next lines until match again
                    if (pCommit is not null)
                    {
                        pCommits.Add(pCommit);
                        pCommit = new Commit {Hash = line.Split(' ').LastOrDefault()};
                    }
                    else
                    {
                        pCommit = new Commit {Hash = line.Split(' ').LastOrDefault()};
                    }
                }
                else
                {
                    if (line.Contains("Author: "))
                    {
                        pCommit.Author = line.Split("Author: ", StringSplitOptions.RemoveEmptyEntries)
                            .FirstOrDefault();
                    }
                    else if (line.Contains("Merge: "))
                    {
                        pCommit.IsMerge = true;
                    }
                    else if (line.Contains("Date: "))
                    {
                        pCommit.Date =
                            DateTime.Parse(line.Split("Date: ", StringSplitOptions.RemoveEmptyEntries)
                                .FirstOrDefault()).ToUniversalTime();
                    }
                    else
                    {
                        pCommit.Message += $"{line?.ToLower().TrimStart().StripPunctuation()} ";
                    }
                }
            }

            await gitlog;
            // foreach (var commit in pCommits.Where(c => !c.IsMerge))
            // { 
            //     Log.Information("{@Commit}", commit);
            // }

            return pCommits.Where(c => !c.IsMerge).ToList();
        }
        catch (Exception e)
        {
            Log.Error(e, "Error parsing repository, please inspect the logs for more information");
            throw;
        }
    }
}