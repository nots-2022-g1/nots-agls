using frontend.Models;
using Refit;

namespace frontend.Services;

public class RepositoryService : IRepositoryService
{
    private readonly IRepositoryService _client;

    public RepositoryService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<IRepositoryService>(httpClient, new RefitSettings());
    }

    public async Task<List<Repository>> Get()
    {
        return await _client.Get();
    }

    public async Task Create(Repository repository)
    {
        await _client.Create(repository);
    }
}