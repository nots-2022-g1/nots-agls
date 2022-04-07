using frontend.Models;
using Refit;

namespace frontend.Services;

public class MachineLearningService : IMachineLearningService
{
    private readonly IMachineLearningService _client;

    public MachineLearningService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<IMachineLearningService>(httpClient, new RefitSettings());
    }

    public async Task<IApiResponse<string>> UploadTrainingData(UploadTrainingDataDto dto)
    {
        return await _client.UploadTrainingData(dto);
    }

    public async Task<IApiResponse<bool>> TrainModel(TrainModelDto dto)
    {
        return await _client.TrainModel(dto);
    }

    public async Task<IApiResponse<string>> Predict(PredictDto dto)
    {
        return await _client.Predict(dto);
    }
}