using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Datasets;

public interface IEditDatasetViewModel
{
    public List<LabeledData> LabeledData { get; }
    public Task FetchLabeledData(int datasetId);
}

public class EditDatasetViewModel : IEditDatasetViewModel
{
    private readonly IDatasetService _datasetService;

    public EditDatasetViewModel(IDatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    public List<LabeledData> LabeledData { get; private set; } = new();
    
    public async Task FetchLabeledData(int datasetId)
    {
        LabeledData = await _datasetService.GetLabeledData(datasetId);
    }
}