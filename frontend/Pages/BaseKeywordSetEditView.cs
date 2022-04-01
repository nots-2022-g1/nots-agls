using frontend.Models;
using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetEditView : ComponentBase
{
    [Inject] protected IKeywordSetEditViewModel ViewModel { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;
    [Parameter] public int KeywordSetId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.FetchKeywordSetAsync(KeywordSetId);
    }
    protected async void HandleValidSubmit()
    {
        await ViewModel.UpdateKeywordSetAsync();
        NavManager.NavigateTo($"keywordsets/{KeywordSetId}");
    }
}