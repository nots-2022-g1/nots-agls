using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetListView : ComponentBase
{
    [Inject] protected IKeywordSetListViewModel ViewModel { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await ViewModel.RetrieveKeywordSetsAsync();
    }

    protected void HandleAddClick()
    {
        NavManager.NavigateTo($"keywordsets/create");
    }
}