using api.Model;

namespace api.Services;

public interface IKeywordsSetService : IGenericCrudService<GenericCrudModel>
{
    Task<IList<Keyword>> GetKeywords(int id);
}