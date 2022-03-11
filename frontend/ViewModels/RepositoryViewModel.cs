using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels;

public class RepositoryViewModel : IRepositoryViewModel
{
	private readonly RepositoryService _repositoryService;

    public RepositoryViewModel(RepositoryService repositoryService)
	{
		_repositoryService = repositoryService;
        Repositories = new();
    }

	public List<Repository> Repositories { get; set; }

    public Repository Repository { get; set; }

    public bool IsEmpty { get => Repositories.Count < 1; }

    public void PrepareRepository()
    {
        Repository = new Repository();
    }

    public async Task<Repository> CreateRepositoryAsync()
    {
        await _repositoryService.CreateRepositoryAsync(Repository);
        return Repository;
    }

    public async Task LoadRepositoriesAsync()
    {
        var result = await _repositoryService.GetRepositoriesAsync();
        Repositories = result.ToList();
    }
}