using frontend.Models;
using Refit;

namespace frontend.Services;

public interface IKeywordService
{
    [Get("/keywordsets")]
    Task<List<KeywordSet>> Get();
    
    [Get("/keywordsets/{id}")]
    Task<KeywordSet> GetById(int id);
    
    [Get("/keywordsets/{id}/keywords")]
    Task<List<Keyword>> GetKeywords(int id);
    
    [Post("/keywordsets/{id}/keywords")]
    Task<Keyword> AddKeyword(int id, KeywordDto dto);
}