using frontend.Models;
using Refit;

namespace frontend.Services;

public interface IRepositoryService
{
    [Get("/repos")]
    Task<List<Repository>> Get();

    [Post("/repos")]
    Task Create(Repository repository);
}