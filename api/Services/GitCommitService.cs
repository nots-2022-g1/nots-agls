using api.Models;
using api.Utils;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace api.Services;

public class GitCommitService : IGitCommitService
{
    private readonly ApplicationContext _context;
    private readonly DbSet<GitCommit> _gitCommits;

    public GitCommitService(ApplicationContext context)
    {
        _context = context;
        _gitCommits = _context.Set<GitCommit>();
    }

    public async Task<IList<GitCommit>> Get(int repoId)
    {
        return await _gitCommits.Where(e => e.GitRepoId.Equals(repoId)).ToListAsync();
    }

    public async Task<PaginatedList<GitCommit>> GetPaginated(int pageIndex, int pageSize)
    {
        var query = _gitCommits.AsQueryable();
        var paginatedList = await PaginatedList<GitCommit>.CreateAsync(query, pageIndex, pageSize);
        return paginatedList;
    }

    public Task<GitCommit> Create(GitCommit commit)
    {
        throw new NotImplementedException();
    }

    public async Task Create(IEnumerable<GitCommit> commits)
    {
        _gitCommits.AddRange(commits);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            Log.Error( "one or more commits already exist");
            throw;
        }
    }

    public Task<GitCommit> Update(GitCommit commit)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
}