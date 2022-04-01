using frontend.Models;
using Refit;

namespace frontend.Services;

public class KeywordService : IKeywordService
{
    private readonly IKeywordService _client;

    public KeywordService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<IKeywordService>(httpClient, new RefitSettings());
    }
    
    public Task<List<KeywordSet>> Get()
    {
        return _client.Get();
    }

    public Task<KeywordSet> GetById(int id)
    {
        return _client.GetById(id);
    }

    public Task<List<Keyword>> GetKeywords(int id)
    {
        return _client.GetKeywords(id);
    }

    public Task<Keyword> AddKeyword(int id, KeywordDto dto)
    {
        return _client.AddKeyword(id, dto);
    }

    public Task<KeywordSet> AddKeywordSet(KeywordSetDto dto)
    {
        return _client.AddKeywordSet(dto);
    }

    public Task RemoveKeywordSet(int id)
    {
        return _client.RemoveKeywordSet(id);
    }

    public Task<KeywordSet> UpdateKeywordSet(int id, KeywordSetDto dto)
    {
        return _client.UpdateKeywordSet(id, dto);
    }
}