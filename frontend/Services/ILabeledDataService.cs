using frontend.Models;
using Refit;

namespace frontend.Services;

public interface ILabeledDataService
{
    [Get("/datasets/{datasetId}/labeledData")]
    Task<List<LabeledData>> Get(int datasetId);

    [Get("/datasets/{datasetId}/labeledData/{id}")]
    Task<LabeledData> GetById(int datasetId, int id);
    
    [Post("/datasets/{datasetId}/labeledData")]
    Task Create(LabeledData labeledData);

    [Put("/datasets/{datasetId}/labeledData/{id}")]
    Task Update(int datasetId, int id, LabeledData labeledData);

    [Delete("/datasets/{datasetId}/labeledData/{id}")]
    Task Delete(int datasetId, int id);
}