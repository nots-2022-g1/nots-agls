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
    Task Create(Repository repository);

    [Put("/repos/{id}")]
    Task Update(int id, Repository repository);

    [Delete("/repos/{id}")]
    Task Delete(int id);
}