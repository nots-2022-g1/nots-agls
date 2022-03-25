using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class KeywordService : IKeywordService
{
    private readonly ApplicationContext _context;
    private readonly DbSet<Keyword> _keywordRepository;

    public KeywordService(ApplicationContext context)
    {
        _context = context;
        _keywordRepository = context.Set<Keyword>();
    }

    public async Task<IList<Keyword>> Get()
    {
        return await _keywordRepository.ToListAsync();
    }

    public async Task<Keyword?> Get(int id)
    {
        return await _keywordRepository.FirstOrDefaultAsync(r => r.Id.Equals(id));
    }

    public async Task<Keyword> Create(Keyword keyword)
    {
        keyword.CreatedAt = DateTime.UtcNow;
        keyword.LastModifiedAt = DateTime.UtcNow;

        var entity = _keywordRepository.Add(keyword);
        await _context.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task<Keyword> Update(Keyword keyword)
    {
        keyword.LastModifiedAt = DateTime.UtcNow;

        var entity = _keywordRepository.Attach(keyword);
        entity.State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return entity.Entity;
    }

    public async Task Delete(int id)
    {
        _keywordRepository.Remove(new Keyword { Id = id });
        await _context.SaveChangesAsync();
    }
}