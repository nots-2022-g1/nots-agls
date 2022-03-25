using frontend.Models;
using frontend.Services;
using frontend.ViewModels.DataSets;

namespace frontend.ViewModels.LabeledDatas;

public interface IListLabeledDataViewModel
{
    public List<LabeledData> LabeledDatas { get; }
    public Task GetLabelsForDataset(int datasetId);
}

public class ListLabeledDataViewModel : IListLabeledDataViewModel
{
    private readonly ILabeledDataService _labeledDataService;

    public ListLabeledDataViewModel(ILabeledDataService labeledDataService)
    {
        _labeledDataService = labeledDataService;
    }

    public List<LabeledData> LabeledDatas { get; private set; } = new();
    
    public async Task GetLabelsForDataset(int datasetId)
    {
        LabeledDatas = await _labeledDataService.Get(datasetId);
    }
}