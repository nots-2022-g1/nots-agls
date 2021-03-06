using api.Models;

namespace api.Services;

public interface ILabeledDataService
{
    Task<LabeledData?> Get(int dataSetId, int id);
    Task<IList<LabeledData>> Get(int dataSetId);
    Task<LabeledData> Create(LabeledData labeledData);
    Task<LabeledData> Update(LabeledData labeledData);
    Task Delete(int dataSetId, int id);
    Task Add(IEnumerable<LabeledData> data);
}