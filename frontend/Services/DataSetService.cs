using frontend.Models;
using Refit;

namespace frontend.Services;

public class DataSetService : IDataSetService
{
    private readonly IDataSetService _client;

    public DataSetService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<IDataSetService>(httpClient, new RefitSettings());
    }
    
    public async Task<List<DataSet>> Get()
    {
        return await _client.Get();
    }

    public async Task<DataSet> GetById(int id)
    {
        return await _client.GetById(id);
    }

    public async Task Create(DataSet dataSet)
    {
        await _client.Create(dataSet);
    }

    public async Task Update(int id, DataSet dataSet)
    {
        await _client.Update(id, dataSet);
    }

    public async Task Delete(int id)
    {
        await _client.Delete(id);
    }
}