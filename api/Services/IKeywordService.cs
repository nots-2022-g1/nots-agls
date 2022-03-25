using api.Model;

namespace api.Services;

public interface IKeywordService
{
    Task<IList<Keyword>> Get();
    Task<Keyword?> Get(int id);
    Task<Keyword> Create(Keyword label);
    Task<Keyword> Update(Keyword label);
    Task Delete(int id);
}