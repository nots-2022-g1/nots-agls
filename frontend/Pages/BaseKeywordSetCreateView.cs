using frontend.Models;
using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetCreateView : ComponentBase
{
    [Inject] protected IKeywordSetCreateViewModel ViewModel { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;
    [Parameter] public int KeywordSetId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ViewModel.KeywordSetDto = new KeywordSetDto();
    }

    protected void HandleValidSubmit()
    {
        Console.WriteLine("HandleValidSubmit called");
        ViewModel.AddKeywordSetAsync();
        NavManager.NavigateTo($"keywordsets");
    }
}