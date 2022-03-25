using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface IViewDataSetViewModel
{
    public DataSet DataSet { get; set; }
    public Task LoadDataSetAsync(int id);
}

public class ViewDataSetViewModel : IViewDataSetViewModel
{
    private readonly IDataSetService _dataSetService;

    public ViewDataSetViewModel(IDataSetService dataSetService)
    {
        _dataSetService = dataSetService;
    }

    public DataSet DataSet { get; set; } = new();

    public async Task LoadDataSetAsync(int id)
    {
        DataSet = await _dataSetService.GetById(id);
    }
}