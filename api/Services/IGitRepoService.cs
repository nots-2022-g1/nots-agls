using api.Models;

namespace api.Services;

public interface IGitRepoService
{
    Task<GitRepo?> Get(int id);
    Task<IList<GitRepo>> Get();
    Task<GitRepo> Create(GitRepo repo);
    Task<GitRepo> Update(GitRepo repo);
    Task Delete(int id);
}