using frontend.Models;
using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetAddView : ComponentBase
{
    [Inject] protected IKeywordSetAddViewModel ViewModel { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;
    [Parameter] public int KeywordSetId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.RetrieveKeywordSetAsync(KeywordSetId);
        ViewModel.Dto = new KeywordDto {KeywordSetId = ViewModel.KeywordSet.Id};
    }

    protected void HandleValidSubmit()
    {
        Console.WriteLine("HandleValidSubmit called");
        ViewModel.AddKeywordAsync();
        NavManager.NavigateTo($"keywordsets/{KeywordSetId}");
    }
}