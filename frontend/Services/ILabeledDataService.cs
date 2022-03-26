using frontend.Models;
using Refit;

namespace frontend.Services;

public interface ILabeledDataService
{
    [Get("/datasets/{dataSetId}/labeledData")]
    Task<List<LabeledData>> Get([AliasAs("dataSetId")] int datasetId);
}