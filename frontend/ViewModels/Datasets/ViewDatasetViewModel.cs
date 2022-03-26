using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface IViewDatasetViewModel
{
    public Dataset Dataset { get; set; }
    public Task LoadDatasetAsync(int id);
}

public class ViewDatasetViewModel : IViewDatasetViewModel
{
    private readonly IDatasetService _datasetService;

    public ViewDatasetViewModel(IDatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    public Dataset Dataset { get; set; } = new();

    public async Task LoadDatasetAsync(int id)
    {
        Dataset = await _datasetService.GetById(id);
    }
}