using Refit;
using frontend.Models;

namespace frontend.Services;

public interface IMachineLearningService
{
    [Post("/machinelearning/loadTrainingData")]
    Task<IApiResponse<string>> UploadTrainingData(UploadTrainingDataDto dto);

    [Post("/machinelearning/trainModel")]
    Task<IApiResponse<bool>> TrainModel(TrainModelDto dto);

    [Post("/machinelearning/predict")]
    Task<IApiResponse<string>> Predict(PredictDto dto);
}