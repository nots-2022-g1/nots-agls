using frontend.Models;

namespace frontend.ViewModels;

public interface IRepositoryViewModel
{
    bool IsEmpty { get; }

    IList<Repository> Repositories { get; set; }

    Task LoadRepositoriesAsync();

    Task<Repository> CreateRepositoryAsync();

    Repository Repository { get; set; }

    void PrepareRepository();
}