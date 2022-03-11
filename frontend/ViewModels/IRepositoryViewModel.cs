using frontend.Models;

namespace frontend.ViewModels;

public interface IRepositoryViewModel
{
    bool IsEmpty { get; }

    List<Repository> Repositories { get; set; }

    Task LoadRepositoriesAsync();

    Task<Repository> CreateRepositoryAsync(Repository repository);
}