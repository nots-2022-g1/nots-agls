using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface IListDatasetsViewModel
{
    public List<Dataset> Datasets { get; }
    public Task FetchDatasetsAsync();
}

public class ListDatasetsViewModel : IListDatasetsViewModel
{
    private readonly IDatasetService _datasetService;

    public ListDatasetsViewModel(IDatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    public List<Dataset> Datasets { get; private set; } = new();
    
    public async Task FetchDatasetsAsync()
    {
        Datasets = await _datasetService.Get();
    }
}