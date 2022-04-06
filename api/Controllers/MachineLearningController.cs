using api.Model;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class MachineLearningController : ControllerBase
{
    private readonly IMachineLearningService _machineLearningService;

    public MachineLearningController(IMachineLearningService machineLearningService)
    {
        _machineLearningService = machineLearningService;
    }

    [HttpPost("/loadDataset")]
    public Task<IActionResult> LoadDataset(LoadDatasetDto dto)
    {
        var trained = _machineLearningService.LoadAndTrainDataset(dto.SaveToFile);
        var result = Ok(trained
            ? "Dataset has been loaded and trained successfully"
            : "This dataset was already used to train this model"
        );
        return Task.FromResult<IActionResult>(result);
    }

    [HttpPost("/classify")]
    public Task<IActionResult> ClassifyCommit(ClassifyDto dto)
    {
        var response = _machineLearningService.PredictClassification(dto.Message);
        var result = Ok(response);
        return Task.FromResult<IActionResult>(result);
    }
}