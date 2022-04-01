using frontend.Models;
using frontend.Services;
using Mapster;

namespace frontend.ViewModels;

public interface IKeywordSetEditViewModel
{
    KeywordSet KeywordSet { get; set; }
    Task FetchKeywordSetAsync(int id);
    Task UpdateKeywordSetAsync();
}

public class KeywordSetEditViewModel : IKeywordSetEditViewModel
{
    private readonly IKeywordService _keywordService;
    public KeywordSet KeywordSet { get; set; }

    public KeywordSetEditViewModel(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    public async Task FetchKeywordSetAsync(int id)
    {
        KeywordSet = await _keywordService.GetById(id);
    }

    public async Task UpdateKeywordSetAsync()
    {
        KeywordSet = await _keywordService.UpdateKeywordSet(KeywordSet.Id, KeywordSet.Adapt<KeywordSetDto>());
    }
}