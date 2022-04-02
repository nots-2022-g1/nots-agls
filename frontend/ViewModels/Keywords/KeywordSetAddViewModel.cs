using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels;

public interface IKeywordSetAddViewModel
{
    KeywordDto Dto { get; set; }
    Keyword Keyword { get; set; }
    KeywordSet KeywordSet { get; set; }
    Task RetrieveKeywordSetAsync(int id);
    Task AddKeywordAsync();
}

public class KeywordSetAddViewModel : IKeywordSetAddViewModel
{
    private readonly IKeywordService _keywordService;
    public KeywordDto Dto { get; set; }
    public Keyword Keyword { get; set; }
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

    public KeywordSetAddViewModel(IKeywordService keywordService)
    {
        Console.WriteLine("KeywordSetAddViewModel constructed.");
        _keywordService = keywordService;
    }

    public async Task RetrieveKeywordSetAsync(int id)
    {
        _KeywordSet = await _keywordService.GetById(id);
        Console.WriteLine($"KeywordSet {id} retrieved.");
    }

    public async Task AddKeywordAsync()
    {
        Keyword = await _keywordService.AddKeyword(KeywordSet.Id, Dto);
        Console.WriteLine($"Keyword \"{Keyword.Name}\" added to \"{KeywordSet.Name}\"");
    }
}