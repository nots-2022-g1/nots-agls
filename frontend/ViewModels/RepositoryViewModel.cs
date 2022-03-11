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

    public bool IsEmpty { get => Repositories.Count < 1; }

    public async Task<Repository> CreateRepositoryAsync(Repository repository)
    {
        await _repositoryService.CreateRepositoryAsync(repository);
        return repository;
    }

    public async Task LoadRepositoriesAsync()
    {
        var result = await _repositoryService.GetRepositoriesAsync();
        Repositories= result.ToList();
    }
}