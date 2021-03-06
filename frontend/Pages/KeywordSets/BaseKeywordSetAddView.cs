using frontend.Models;
using frontend.ViewModels.KeywordSets;
using Microsoft.AspNetCore.Components;

namespace frontend.Pages.KeywordSets;

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

    protected async void HandleValidSubmit()
    {
        await ViewModel.AddKeywordAsync();
        NavManager.NavigateTo($"keywordsets/{KeywordSetId}/edit");
    }
}