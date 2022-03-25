using Refit;
using frontend.Models;

namespace frontend.Services;

public class KeywordService : IKeywordService
{
    private readonly IKeywordService _client;

    public KeywordService(IConfiguration config, HttpClient httpClient)
    {

        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        this._client = RestService.For<IKeywordService>(httpClient, new RefitSettings());
    }

    public async Task<ApiResponse<Keyword>> Create(KeywordDto keyword)
    {
        return await this._client.Create(keyword);
    }

    public async Task<List<Keyword>> Get()
    {
        return await this._client.Get();
    }

    public async Task<Keyword> GetById(int id)
    {
        return await this._client.GetById(id);
    }

    public async Task<ApiResponse<Keyword>> Update(int id, KeywordDto keyword)
    {
        return await this._client.Update(id, keyword);
    }

    public async Task<ApiResponse<Keyword>> Delete(int id)
    {
        return await this._client.Delete(id);
    }
}