using frontend.Models;
using Refit;

namespace frontend.Services;

public interface IRepositoryService
{
    [Get("/repos")]
    Task<List<Repository>> Get();

    [Get("/repos/{id}")]
    Task<Repository> GetById(int id);

    [Post("/repos")]
    Task Create(GitRepoCreateDto repository);
    
    [Post("/repos/multi")]
    Task Create(ICollection<GitRepoCreateDto> repository);

    [Put("/repos/{id}")]
    Task Update(int id, Repository repository);

    [Delete("/repos/{id}")]
    Task Delete(int id);
}