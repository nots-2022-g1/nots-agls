using api.Model;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<GitCommit>> Get(int repoId)
    {
        return await _gitCommits.Where(e => e.GitRepoId.Equals(repoId)).ToListAsync();
    }

    public Task<GitCommit> Create(GitCommit commit)
    {
        throw new NotImplementedException();
    }

    public async Task Create(IEnumerable<GitCommit> commits)
    {
        _gitCommits.AddRange(commits);
        await _context.SaveChangesAsync();
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