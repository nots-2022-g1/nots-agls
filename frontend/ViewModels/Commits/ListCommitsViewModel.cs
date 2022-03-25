using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Commits;

public interface IListCommitsViewModel
{
    public List<Commit> Commits { get; }
    public Task FetchCommitsForRepoAsync(int repoId);
}

public class ListCommitsViewModel : IListCommitsViewModel
{
    private readonly ICommitService _commitService;

    public ListCommitsViewModel(ICommitService commitService)
    {
        _commitService = commitService;
    }

    public List<Commit> Commits { get; private set; } = new();

    public async Task FetchCommitsForRepoAsync(int repoId)
    {
        Commits = await _commitService.GetByRepoId(repoId);
    }
}
