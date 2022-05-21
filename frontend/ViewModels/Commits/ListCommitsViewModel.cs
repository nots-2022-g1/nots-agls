using frontend.Models;
using frontend.Services;
using frontend.Utils;

namespace frontend.ViewModels.Commits;

public interface IListCommitsViewModel
{
    public PaginatedList<GitCommit> Commits { get; }
    public Task FetchCommitsForRepoAsync(int repoId, int pageId);
}

public class ListCommitsViewModel : IListCommitsViewModel
{
    private readonly ICommitService _commitService;

    public ListCommitsViewModel(ICommitService commitService)
    {
        _commitService = commitService;
    }

    public PaginatedList<GitCommit> Commits { get; private set; } = new();

    public async Task FetchCommitsForRepoAsync(int repoId, int pageId)
    {
        Commits = await _commitService.GetByRepoIdPaginated(repoId, pageId);
    }
}
