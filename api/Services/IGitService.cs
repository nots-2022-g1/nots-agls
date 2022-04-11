using api.Model;

namespace api.Services;

/*
 * This service handles all git wrapper operations
 */
public interface IGitService
{
    Task<IEnumerable<string>> ListRepos();
    Task<Uri> Clone(GitRepo repo);
    Task Pull(GitRepo repo);
    Task<string> Log(GitRepo repo);
    Task<IEnumerable<string>> ListBranches(GitRepo repo);
    Task SwitchToBranch(GitRepo repo, string branch);
}