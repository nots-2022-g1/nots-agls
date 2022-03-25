using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface ICreateDataSetViewModel
{
    public DataSet DataSet { get; set; }
    public Task CreateDataSetAsync();
}

public class CreateDataSetViewModel : ICreateDataSetViewModel
{
    private readonly IDataSetService _dataSetService;

    public CreateDataSetViewModel(IDataSetService dataSetService)
    {
        _dataSetService = dataSetService;
    }
    
    public DataSet DataSet { get; set; } = new();
    
    public async Task CreateDataSetAsync()
    {
        await _dataSetService.Create(DataSet);
    }
}