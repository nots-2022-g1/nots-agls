using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface IEditDatasetViewModel
{
    public Dataset Dataset { get; set; }
    public Task LoadDatasetAsync(int id);
    public Task SubmitEditAsync(int id);
}

public class EditDatasetViewModel : IEditDatasetViewModel
{
    private readonly IDatasetService _datasetService;

    public EditDatasetViewModel(IDatasetService datasetService)
    {
        _datasetService = datasetService;
    }

    public Dataset Dataset { get; set; } = new();

    public async Task LoadDatasetAsync(int id)
    {
        Dataset = await _datasetService.GetById(id);
    }

    public async Task SubmitEditAsync(int id)
    {
        var dto = new DatasetDto
        {
            Name = Dataset.Name
        };
        await _datasetService.Update(id, dto);
    }
}