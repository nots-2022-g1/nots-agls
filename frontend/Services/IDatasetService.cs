using Refit;
using frontend.Models;

namespace frontend.Services;

public interface IDatasetService
{
    [Post("/datasets")]
    Task<ApiResponse<Dataset>> Create(DatasetDto datasetDto);

    [Get("/datasets")]
    Task<List<Dataset>> Get();

    [Get("/datasets/{id}")]
    Task<Dataset> GetById(int id);

    [Get("/datasets/{id}/labeledData")]
    Task<List<LabeledData>> GetLabeledData(int id);

    [Put("/datasets/{id}")]
    Task<ApiResponse<Dataset>> Update(int id, DatasetDto datasetDto);

    [Delete("/datasets/{id}")]
    Task<ApiResponse<Dataset>> Delete(int id);
    Task<string> GenerateTsvAsync(int id);

    [Post("/datasets/autolabel")]
    Task AutoLabel(AutoLabelConfig config);
}
