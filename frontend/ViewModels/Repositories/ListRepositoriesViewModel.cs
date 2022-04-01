using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Repositories;

public interface IListRepositoriesViewModel
{
    public List<Repository> Repositories { get; }
    public Task FetchRepositoriesAsync();
    public Task DeleteRepositoryAsync(int id);
}

public class ListRepositoriesViewModel : IListRepositoriesViewModel
{
    private readonly IRepositoryService _repositoryService;

    public ListRepositoriesViewModel(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }
    
    public List<Repository> Repositories { get; private set; } = new();

    public async Task FetchRepositoriesAsync()
    {
        Repositories = await _repositoryService.Get();
    }

    public async Task DeleteRepositoryAsync(int id)
    {
        await _repositoryService.Delete(id);
        Repositories = await _repositoryService.Get();
    }
}