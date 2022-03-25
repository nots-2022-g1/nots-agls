using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Repositories;

public interface IViewRepositoryViewModel
{
    public Repository Repository { get; set; }
    public Task LoadRepositoryAsync(int id);
}

public class ViewRepositoryViewModel : IViewRepositoryViewModel
{
    private readonly IRepositoryService _repositoryService;

    public ViewRepositoryViewModel(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }

    public Repository Repository { get; set; } = new();

    public async Task LoadRepositoryAsync(int id)
    {
        Repository = await _repositoryService.GetById(id);
    }
}