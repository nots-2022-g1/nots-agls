using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetView : ComponentBase
{
    [Inject] protected IKeywordSetDetailViewModel KeywordSetDetailViewModel { get; set; } = default!;

    [Inject] protected NavigationManager NavManager { get; set; } = default!;

    [Parameter] public int KeywordSetId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await KeywordSetDetailViewModel.RetrieveKeywordSetAsync(KeywordSetId);
        await KeywordSetDetailViewModel.RetrieveKeywordsAsync();
    }

    protected void HandleAddClick()
    {
        NavManager.NavigateTo($"keywordsets/{KeywordSetId}/add");
    }
}