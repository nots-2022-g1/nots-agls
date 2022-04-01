using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class KeywordService : GenericCrudService<Keyword>, IKeywordService
{
    private readonly ApplicationContext _context;

    public KeywordService(ApplicationContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IList<Keyword>> GetByKeywordSetId(int keywordSetId)
    {
        return await _repo.Where(e => e.KeywordSetId.Equals(keywordSetId)).ToListAsync();
    }
}