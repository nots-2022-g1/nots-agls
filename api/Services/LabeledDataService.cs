using api.Model;

namespace api.Services;

public class LabeledDataService : ILabeledDataService
{
    public Task<IList<LabeledData>> Get(int dataSetId)
    {
        throw new NotImplementedException();
    }

    public Task<LabeledData> Create(LabeledData labeledData)
    {
        throw new NotImplementedException();
    }

    public Task<LabeledData> Update(LabeledData labeledData)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}