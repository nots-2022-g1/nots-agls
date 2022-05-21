using frontend.Models;
using frontend.Utils;
using Refit;

namespace frontend.Services
{
    public interface ICommitService
    {
        [Get("/repos/{repoId}/commits")]
        Task<List<GitCommit>> GetByRepoId(int repoId);
        [Get("/repos/{repoId}/commits/page/{pageId}")]
        Task<PaginatedList<GitCommit>> GetByRepoIdPaginated(int repoId, int pageId);
    }
}
