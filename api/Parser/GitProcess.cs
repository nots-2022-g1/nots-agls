using System.Diagnostics;

namespace api.Parser;

public class GitProcess : Process
{
    public GitProcess(string workingDirectory, string args)
    {
        StartInfo.UseShellExecute = false;
        StartInfo.FileName = "git";
        StartInfo.CreateNoWindow = true;
        StartInfo.WorkingDirectory = workingDirectory;
        StartInfo.Arguments = args;
        StartInfo.RedirectStandardOutput = true;
    }
}