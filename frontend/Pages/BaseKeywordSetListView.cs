using frontend.ViewModels;
using Microsoft.AspNetCore.Components;
using Serilog;

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
        NavManager.NavigateTo($"keywordsets/add");
    }
    
    protected async void HandleRemoveClick(int id)
    {
        await ViewModel.RemoveKeywordSetAsync(id);
        await ViewModel.RetrieveKeywordSetsAsync();
        StateHasChanged();
    }
}