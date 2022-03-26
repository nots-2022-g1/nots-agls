using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.DataSets;

public interface ICreateDatasetViewModel
{
    public Dataset Dataset { get; set; }
    public Task CreateDatasetAsync();
}

public class CreateDatasetViewModel : ICreateDatasetViewModel
{
    private readonly IDatasetService _datasetService;

    public CreateDatasetViewModel(IDatasetService datasetService)
    {
        _datasetService = datasetService;
    }
    
    public Dataset Dataset { get; set; } = new();
    
    public async Task CreateDatasetAsync()
    {
        var dto = new DatasetDto
        {
            Name = Dataset.Name
        };
        await _datasetService.Create(dto);
    }
}