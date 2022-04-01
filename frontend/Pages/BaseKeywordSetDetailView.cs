using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetDetailView : ComponentBase
{
    [Inject] protected IKeywordSetDetailViewModel ViewModel { get; set; } = default!;

    [Inject] protected NavigationManager NavManager { get; set; } = default!;

    [Parameter] public int KeywordSetId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.RetrieveKeywordSetAsync(KeywordSetId);
        await ViewModel.RetrieveKeywordsAsync();
    }

    protected void HandleAddClick()
    {
        NavManager.NavigateTo($"keywordsets/{KeywordSetId}/add");
    }
}