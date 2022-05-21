using api.Models;
using api.Utils;

namespace api.Services;

public interface IGitCommitService
{
    Task<IList<GitCommit>> Get(int repoId);
    Task<PaginatedList<GitCommit>> GetPaginated(int pageIndex, int pageSize);
    Task Create(IEnumerable<GitCommit> commits);
    Task Delete(int id);
}