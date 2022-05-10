using System.Diagnostics;
using api.Models;
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

    [HttpPost("loadTrainingData")]
    public Task<IActionResult> LoadTrainingData(LoadTrainingDataDto dto)
    {
        var fileId = _machineLearningService.LoadTrainingData(dto.FileName, dto.FileContent);
        return Task.FromResult<IActionResult>(Ok(fileId));
    }

    [HttpPost("trainModel")]
    public Task<IActionResult> TrainModel(TrainModelDto dto)
    {
        var success = _machineLearningService.TrainModel(dto.FileId, dto.TrainingSet);
        return Task.FromResult<IActionResult>(Ok(success));
    }

    [HttpPost("predict")]
    public Task<IActionResult> Predict(PredictDto dto)
    {
        var response = _machineLearningService.Predict(dto.FileId, dto.TrainingSet, dto.Value);
        return Task.FromResult<IActionResult>(Ok(response));
    }
    
}