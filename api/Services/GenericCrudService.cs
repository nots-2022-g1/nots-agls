using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class GenericCrudService<T> : IGenericCrudService<T> where T : class, IGenericCrudModel, new()
{
    private readonly ApplicationContext _context;
    protected readonly DbSet<T> _repo;

    public GenericCrudService(ApplicationContext context)
    {
        _context = context;
        _repo = context.Set<T>();
    }

    public async Task<T?> Get(int id)
    {
        return await _repo.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<IList<T>> Get()
    {
        return await _repo.ToListAsync();
    }

    public async Task<T> Create(T entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        entity.LastModifiedAt = DateTime.UtcNow;
        var created = _repo.Add(entity);
        await _context.SaveChangesAsync();
        return created.Entity;
    }

    public async Task<T> Update(T entity)
    {
        entity.LastModifiedAt = DateTime.UtcNow;
        var modified = _repo.Attach(entity);
        modified.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return modified.Entity;
    }

    public async Task Delete(int id)
    {
        var deleted = _repo.Attach(new T {Id = id});
        deleted.State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}