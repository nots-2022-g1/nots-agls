using api.Model;

namespace api.Services;

public interface IGitRepoService
{
    Task<IList<GitRepo>> Get();
    Task<GitRepo> Create(GitRepo repo);
    Task<GitRepo> Update(GitRepo repo);
    Task Delete(int id);
}