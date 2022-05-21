using frontend.Models;
using frontend.Utils;
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

    public async Task<List<GitCommit>> GetByRepoId(int repoId)
    {
        return await _client.GetByRepoId(repoId);
    }

    public async Task<PaginatedList<GitCommit>> GetByRepoIdPaginated(int repoId, int pageId)
    {
        return await _client.GetByRepoIdPaginated(repoId, pageId);
    }
}
