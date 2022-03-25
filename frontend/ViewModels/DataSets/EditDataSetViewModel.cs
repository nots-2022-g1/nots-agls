using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface IEditDataSetViewModel
{
    public DataSet DataSet { get; set; }
    public Task LoadDataSetAsync(int id);
    public Task SubmitEditAsync(int id);
}

public class EditDataSetViewModel : IEditDataSetViewModel
{
    private readonly IDataSetService _dataSetService;

    public EditDataSetViewModel(IDataSetService dataSetService)
    {
        _dataSetService = dataSetService;
    }

    public DataSet DataSet { get; set; } = new();

    public async Task LoadDataSetAsync(int id)
    {
        DataSet = await _dataSetService.GetById(id);
    }

    public async Task SubmitEditAsync(int id)
    {
        await _dataSetService.Update(id, DataSet);
    }
}