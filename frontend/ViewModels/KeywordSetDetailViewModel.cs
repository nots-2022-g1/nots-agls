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
    private KeywordSet _KeywordSet;

    public KeywordSet KeywordSet
    {
        get => _KeywordSet;
        set => _KeywordSet = value;
    }
    private List<Keyword> _keywords;
    public List<Keyword> Keywords 
    { 
        get => _keywords; 
        set => _keywords = value; 
    }

    public KeywordSetDetailViewModel(IKeywordService keywordService)
    {
        Console.WriteLine("ListKeywordSetsViewModel constructed.");
        _keywordService = keywordService;
    }

    public async Task RetrieveKeywordSetAsync(int id)
    {
        _KeywordSet = await _keywordService.GetById(id);
        Console.WriteLine($"KeywordSet {id} retrieved.");
    }

    public async Task RetrieveKeywordsAsync()
    {
        _keywords = await _keywordService.GetKeywords(_KeywordSet.Id);
        Console.WriteLine($"Keywords for KeywordSet {_KeywordSet.Id} retrieved.");
    }
}