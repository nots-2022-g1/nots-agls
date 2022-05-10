using api.Models;
using api.Parser;

namespace api.Services;

public class GitService : IGitService
{
    private readonly IConfiguration _config;
    private readonly string _directory;

    public GitService(IConfiguration config)
    {
        _config = config;
        _directory = Path.Combine(Path.GetTempPath(), "gitrepos");
        Directory.CreateDirectory(_directory);
    }

    public async Task<IEnumerable<string>> ListRepos()
    {
        return await Task.Run(() => Directory.EnumerateDirectories(_directory));
    }

    public async Task<Uri> Clone(GitRepo repo)
    {
        var location = new Uri(Path.Join(_directory, repo.Name));
        if (Directory.Exists(location.LocalPath))
        {
            return location;
        }

        using var gitClone = new GitProcess(_directory, $"clone {repo.Url} {repo.Name}");
        try
        {
            var didStart = gitClone.Start();
            if (didStart)
            {
                Serilog.Log.Information("Started cloning repository {RepoUri} into {RepoName}", repo.Url,
                    repo.Name);
            }
        }
        catch (InvalidOperationException e)
        {
            Serilog.Log.Error(e, "Error cloning git repo");
            throw;
        }

        await gitClone.WaitForExitAsync();
        return location;
    }

    public async Task Pull(GitRepo repo)
    {
        using var gitPull = new GitProcess(Path.Join(_directory, repo.Name), "pull");
        gitPull.Start();
        await gitPull.WaitForExitAsync();
    }

    public async Task<string> Log(GitRepo repo)
    {
        var location = Path.Join(_directory, repo.Name);
        
        if (Directory.Exists(location))
        {
            await Pull(repo);
        }
        else
        {
            await Clone(repo);
        }
        
        using var gitLog = new GitProcess(location, "log --date=iso");
        gitLog.Start();
        var output = await gitLog.StandardOutput.ReadToEndAsync();
        await gitLog.WaitForExitAsync();
        return output;
    }

    public Task<IEnumerable<string>> ListBranches(GitRepo repo)
    {
        throw new NotImplementedException();
    }

    public Task SwitchToBranch(GitRepo repo, string branch)
    {
        throw new NotImplementedException();
    }
}