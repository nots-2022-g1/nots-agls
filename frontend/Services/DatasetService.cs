using Refit;
using frontend.Models;
using Serilog;

namespace frontend.Services;

public class DatasetService : IDatasetService
{
    private readonly IDatasetService _client;

    public DatasetService(IConfiguration config, HttpClient httpClient)
    {

        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<IDatasetService>(httpClient, new RefitSettings());
    }

    public async Task<ApiResponse<Dataset>> Create(DatasetDto dataset)
    {
        Log.Information("TEST");
        Log.Information(dataset.Name);
        return await _client.Create(dataset);
    }

    public async Task<List<Dataset>> Get()
    {
        return await _client.Get();
    }

    public async Task<Dataset> GetById(int id)
    {
        return await _client.GetById(id);
    }

    public async Task<ApiResponse<Dataset>> Update(int id, DatasetDto dataset)
    {
        return await _client.Update(id, dataset);
    }

    public async Task<ApiResponse<Dataset>> Delete(int id)
    {
        return await _client.Delete(id);
    }
}
