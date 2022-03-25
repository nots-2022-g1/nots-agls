using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface IListDataSetsViewModel
{
    public List<DataSet> DataSets { get; }
    public Task FetchDataSetsAsync();
}

public class ListDataSetsViewModel : IListDataSetsViewModel
{
    private readonly IDataSetService _dataSetService;

    public ListDataSetsViewModel(IDataSetService dataSetService)
    {
        _dataSetService = dataSetService;
    }

    public List<DataSet> DataSets { get; private set; } = new();
    
    public async Task FetchDataSetsAsync()
    {
        DataSets = await _dataSetService.Get();
    }
}