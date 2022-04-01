using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels;

public interface IKeywordSetDetailViewModel
{
    List<Keyword> Keywords { get; set; }
    KeywordSet KeywordSet { get; set; }
    Task RetrieveKeywordSetAsync(int id);
    Task RetrieveKeywordsAsync();
}

public class KeywordSetDetailViewModel : IKeywordSetDetailViewModel
{
    private readonly IKeywordService _keywordService;
    public KeywordSet KeywordSet { get; set; }
    public List<Keyword> Keywords { get; set; }

    public KeywordSetDetailViewModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    public async Task RetrieveKeywordSetAsync(int id)
    {
        KeywordSet = await _keywordService.GetById(id);
    }

    public async Task RetrieveKeywordsAsync()
    {
        Keywords = await _keywordService.GetKeywords(KeywordSet.Id);
    }
}