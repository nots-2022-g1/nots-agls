using Refit;
using System.Net.Http.Headers;
using frontend.Models;
using Serilog;

namespace frontend.Services;

public class OpenAIService : IOpenAIService
{
    private readonly IOpenAIService _client;

    public OpenAIService(IConfiguration config, HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri("https://api.openai.com/v1");
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.GetSection("MyAppSettings").GetValue<string>("OpenAI.API.Key"));
        _client = RestService.For<IOpenAIService>(httpClient, new RefitSettings());
    }
    public async Task<OpenAI> getSummary(OpenAISummarizeDTO summarizeDTO)
    {
        return await _client.getSummary(summarizeDTO);        
    }
}