using api.Model;

namespace api.Services;

public interface ILabeledDataService
{
    Task<IList<LabeledData>> Get(int dataSetId);
    Task<LabeledData> Create(LabeledData labeledData);
    Task<LabeledData> Update(LabeledData labeledData);
    Task Delete(int id);
}