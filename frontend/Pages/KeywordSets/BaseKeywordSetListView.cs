using frontend.ViewModels.KeywordSets;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages.KeywordSets;

public class BaseKeywordSetListView : ComponentBase
{
    [Inject] protected IKeywordSetListViewModel ViewModel { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.RetrieveKeywordSetsAsync();
    }
    protected async void HandleRemoveClick(int id)
    {
        await ViewModel.RemoveKeywordSetAsync(id);
        await ViewModel.RetrieveKeywordSetsAsync();
        StateHasChanged();
    }
}