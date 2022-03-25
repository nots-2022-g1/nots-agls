using frontend.Models;
using Refit;

namespace frontend.Services;

public class LabeledDataService : ILabeledDataService
{
    private readonly ILabeledDataService _client;
    
    public LabeledDataService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<ILabeledDataService>(httpClient, new RefitSettings());
    }

    public async Task<List<LabeledData>> Get(int datasetId)
    {
        return await _client.Get(datasetId);
    }

    public Task<LabeledData> GetById(int datasetId, int id)
    {
        throw new NotImplementedException();
    }

    public Task Create(LabeledData labeledData)
    {
        throw new NotImplementedException();
    }

    public Task Update(int datasetId, int id, LabeledData labeledData)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int datasetId, int id)
    {
        throw new NotImplementedException();
    }
}