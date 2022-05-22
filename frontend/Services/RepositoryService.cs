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

    public async Task<Repository> GetById(int id)
    {
        return await _client.GetById(id);
    }

    public async Task Create(GitRepoCreateDto repository)
    {
        await _client.Create(repository);
    }
    
    public async Task Create(ICollection<GitRepoCreateDto> repositories)
    {
        await _client.Create(repositories);
    }

    public async Task Update(int id, Repository repository)
    {
        await _client.Update(id, repository);
    }

    public async Task Delete(int id)
    {
        await _client.Delete(id);
    }

}