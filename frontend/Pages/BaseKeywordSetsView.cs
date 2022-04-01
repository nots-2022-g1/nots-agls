using frontend.ViewModels;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages;

public class BaseKeywordSetsView : ComponentBase
{
    [Inject] 
    protected IListKeywordSetsViewModel ListKeywordSetsViewModel { get; set; } = default!;
    
    protected override async Task OnInitializedAsync()
    {
        await ListKeywordSetsViewModel.RetrieveKeywordSetsAsync();
    }
}