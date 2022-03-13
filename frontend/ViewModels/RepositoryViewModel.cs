using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels;

public class RepositoryViewModel : IRepositoryViewModel
{
	private readonly IRepositoryService _repositoryService;

    public RepositoryViewModel(IRepositoryService repositoryService)
	{
		_repositoryService = repositoryService;
        Repositories = new List<Repository>();
    }

    public IList<Repository> Repositories { get; set; }

    public Repository Repository { get; set; }

    public bool IsEmpty => Repositories.Count < 1;

    public void PrepareRepository()
    {
        Repository = new Repository();
    }

    public async Task<Repository> CreateRepositoryAsync()
    {
        await _repositoryService.Create(Repository);
        return Repository;
    }

    public async Task LoadRepositoriesAsync()
    {
        var result = await _repositoryService.Get();
        Repositories = result.ToList();
    }
}