using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class GitRepoService : IGitRepoService
{
    private readonly ApplicationContext _context;
    private readonly DbSet<GitRepo> _gitRepositories;

    public GitRepoService(ApplicationContext context)
    {
        _context = context;
        _gitRepositories = context.Set<GitRepo>();
    }

    public async Task<GitRepo?> Get(int id)
    {
        return await _gitRepositories.FirstOrDefaultAsync(r => r.Id.Equals(id));
    }
    public async Task<IList<GitRepo>> Get()
    {
        return await _gitRepositories.ToListAsync();
    }

    public async Task<GitRepo> Create(GitRepo repo)
    {
        var entity = _gitRepositories.Add(repo);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<GitRepo> Update(GitRepo repo)
    {
        var entity = _gitRepositories.Attach(repo);
        entity.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task Delete(int id)
    {
        _gitRepositories.Remove(new GitRepo {Id = id});
        await _context.SaveChangesAsync();
    }
}