using Refit;
using frontend.Models;

namespace frontend.Services;

public interface IDatasetService
{
    [Post("/datasets")]
    Task<ApiResponse<Dataset>> Create(DatasetDto label);

    [Get("/datasets")]
    Task<List<Dataset>> Get();

    [Get("/datasets/{id}")]
    Task<Dataset> GetById(int id);

    [Patch("/datasets/{id}")]
    Task<ApiResponse<Dataset>> Update(int id, DatasetDto label);

    [Delete("/datasets/{id}")]
    Task<ApiResponse<Dataset>> Delete(int id);
}
