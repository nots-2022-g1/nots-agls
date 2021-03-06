using api.Models;

namespace api.Services;

public interface IKeywordService : IGenericCrudService<Keyword>
{
    public Task<IList<Keyword>> GetByKeywordSetId(int keywordSetId);
}