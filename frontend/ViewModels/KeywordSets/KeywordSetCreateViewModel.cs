using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels.KeywordSets;

public interface IKeywordSetCreateViewModel
{
    KeywordSetDto KeywordSetDto { get; set; }
    KeywordSet KeywordSet { get; set; }
    Task AddKeywordSetAsync();
}

public class KeywordSetCreateViewModel : IKeywordSetCreateViewModel
{
    private readonly IKeywordService _keywordService;
    public KeywordSetDto KeywordSetDto { get; set; }
    public KeywordSet KeywordSet { get; set; }

    public KeywordSetCreateViewModel(IKeywordService keywordService)
    {
        Console.WriteLine("KeywordSetCreateViewModel constructed.");
        _keywordService = keywordService;
    }

    public async Task AddKeywordSetAsync()
    {
        KeywordSet = await _keywordService.AddKeywordSet(KeywordSetDto);
        Console.WriteLine($"Keywordset \"{KeywordSet.Name}\" added");
    }
}