using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class KeywordsSetService : GenericCrudService<GenericCrudModel>, IKeywordsSetService
{
    private readonly DbSet<Keyword> _keywordsRepo;

    public KeywordsSetService(ApplicationContext context) : base(context)
    {
        _keywordsRepo = context.Set<Keyword>();
    }

    public async Task<IList<Keyword>> GetKeywords(int id)
    {
        return await _keywordsRepo.Where(e => e.KeywordsetId.Equals(id)).ToListAsync();
    }
}