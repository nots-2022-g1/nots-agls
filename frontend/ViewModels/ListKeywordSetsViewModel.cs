using frontend.Models;
using frontend.Services;

namespace frontend.ViewModels;

public interface IListKeywordSetsViewModel
{
    List<KeywordSet> KeywordSets { get; set; }
    Task RetrieveKeywordSetsAsync();
}

public class ListKeywordSetsViewModel : IListKeywordSetsViewModel
{
    private readonly IKeywordService _keywordService;
    private List<KeywordSet> _keywordSets;
    public List<KeywordSet> KeywordSets 
    { 
        get => _keywordSets; 
        set => _keywordSets = value; 
    }

    public ListKeywordSetsViewModel(IKeywordService keywordService)
    {
        Console.WriteLine("ListKeywordSetsViewModel constructed.");
        _keywordService = keywordService;
    }

    public async Task RetrieveKeywordSetsAsync()
    {
        _keywordSets = await _keywordService.Get();
        Console.WriteLine("KeywordSets retrieved.");
    }
}