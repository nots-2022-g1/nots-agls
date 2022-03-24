using Refit;
using frontend.Models;

namespace frontend.Services;

public interface IKeywordService
{
    [Post("/keywords")]
    Task<ApiResponse<Keyword>> Create(KeywordDto label);

    [Get("/keywords")]
    Task<List<Keyword>> Get();

    [Get("/keywords/{id}")]
    Task<Keyword> GetById(int id);

    [Patch("/keywords/{id}")]
    Task<ApiResponse<Keyword>> Update(int id, KeywordDto label);

    [Delete("/keywords/{id}")]
    Task<ApiResponse<Keyword>> Delete(int id);
}
