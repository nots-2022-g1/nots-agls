using frontend.Models;
using frontend.Services;
using Mapster;

namespace frontend.ViewModels.KeywordSets;

public interface IKeywordSetEditViewModel
{
    KeywordSet KeywordSet { get; set; }
    List<Keyword> Keywords { get; set; }
    Task RetrieveKeywordSetAsync(int id);
    Task RetrieveKeywordsAsync();
    Task UpdateKeywordSetAsync();
    Task DeleteKeywordAsync(int id);
}

public class KeywordSetEditViewModel : IKeywordSetEditViewModel
{
    private readonly IKeywordService _keywordService;
    public KeywordSet KeywordSet { get; set; }
    public List<Keyword> Keywords { get; set; }

    public KeywordSetEditViewModel(IKeywordService keywordService)
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

    public async Task UpdateKeywordSetAsync()
    {
        KeywordSet = await _keywordService.UpdateKeywordSet(KeywordSet.Id, KeywordSet.Adapt<KeywordSetDto>());
    }

    public async Task DeleteKeywordAsync(int id)
    {
        await _keywordService.RemoveKeyword(KeywordSet.Id, id);
    }
}