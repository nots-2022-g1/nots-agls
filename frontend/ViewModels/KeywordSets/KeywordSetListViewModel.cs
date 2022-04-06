using System.ComponentModel;
using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.KeywordSets;

public interface IKeywordSetListViewModel
{
    List<KeywordSet> KeywordSets { get; set; }
    Task RetrieveKeywordSetsAsync();
    Task RemoveKeywordSetAsync(int id);
}

public class KeywordSetListViewModel : IKeywordSetListViewModel
{
    private readonly IKeywordService _keywordService;
    public List<KeywordSet> KeywordSets { get; set; }

    public KeywordSetListViewModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    public async Task RetrieveKeywordSetsAsync()
    {
        KeywordSets = await _keywordService.Get();
    }

    public async Task RemoveKeywordSetAsync(int id)
    {
        await _keywordService.RemoveKeywordSet(id);
        
    }
}