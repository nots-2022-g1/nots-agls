using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Commits;

public interface IListCommitsViewModel
{
    public List<Commit> Commits { get; }
    public Task FetchCommitsAsync();
}

public class ListCommitsViewModel : IListCommitsViewModel
{
    private readonly IRepositoryService _repositoryService;

    public ListCommitsViewModel(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }

    public List<Commit> Commits { get; private set; } = new();

    public async Task FetchCommitsAsync()
    {
        //
    }
}
