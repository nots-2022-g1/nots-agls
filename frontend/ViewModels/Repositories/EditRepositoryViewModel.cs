using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.Repositories;

public interface IEditRepositoryViewModel
{
    public Repository Repository { get; set; }
    public Task LoadRepositoryAsync(int id);
    public Task SubmitEditAsync(int id);
}

public class EditRepositoryViewModel : IEditRepositoryViewModel
{
    private readonly IRepositoryService _repositoryService;

    public EditRepositoryViewModel(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }

    public Repository Repository { get; set; } = new();

    public async Task LoadRepositoryAsync(int id)
    {
        Repository = await _repositoryService.GetById(id);
    }

    public async Task SubmitEditAsync(int id)
    {
        await _repositoryService.Update(id, Repository);
    }
}