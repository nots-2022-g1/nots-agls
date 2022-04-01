using frontend.Models;
using Refit;

namespace frontend.Services
{
    public interface ICommitService
    {
        [Get("/repos/{repoId}/commits")]
        Task<List<GitCommit>> GetByRepoId(int repoId);
    }
}
