using frontend.Models;
using Refit;

namespace frontend.Services;

public class CommitService : ICommitService
{
    private readonly ICommitService _client;

    public CommitService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<ICommitService>(httpClient, new RefitSettings());
    }

    public async Task<List<Commit>> GetByRepoId(int repoId)
    {
        return await _client.GetByRepoId(repoId);
    }
}
