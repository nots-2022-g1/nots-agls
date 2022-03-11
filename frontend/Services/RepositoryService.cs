using frontend.Models;

namespace frontend.Services;

public class RepositoryService
{
    private readonly HttpClient _httpClient;

    public RepositoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Repository[]> GetRepositoriesAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<Repository[]>("https://localhost:7097/repos");
        return result;
    }

    public async Task CreateRepositoryAsync(Repository repository)
    {
        var result = await _httpClient.PostAsJsonAsync("https://localhost:7097/repos", repository);
        System.Diagnostics.Debug.WriteLine(result);
    }
}