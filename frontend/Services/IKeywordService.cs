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
    
    [Post("/keywordsets")]
    Task<KeywordSet> AddKeywordSet(KeywordSetDto dto);
    
    [Delete("/keywordsets/{id}")]
    Task RemoveKeywordSet(int id);
    
    [Put("/keywordsets/{id}")]
    Task<KeywordSet> UpdateKeywordSet(int id, KeywordSetDto dto);
    
    [Delete("/keywordsets/{keywordSetId}/keywords/{id}")]
    Task RemoveKeyword(int keywordSetId, int id);
}