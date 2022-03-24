using frontend.Models;
using Refit;

namespace frontend.Services
{
    public interface ICommitService
    {
        [Get("/commits/{repoId:int}")]
        Task<List<Commit>> GetByRepoId(int repoId);
    }
}
