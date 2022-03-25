using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Repositories;

public interface ICreateRepositoryViewModel
{
    public Repository Repository { get; set; }
    public Task CreateRepositoryAsync();
}

public class CreateRepositoryViewModel : ICreateRepositoryViewModel
{
    private readonly IRepositoryService _repositoryService;

    public CreateRepositoryViewModel(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }
    public Repository Repository { get; set; } = new();
    public async Task CreateRepositoryAsync()
    {
        await _repositoryService.Create(Repository);
    }
}