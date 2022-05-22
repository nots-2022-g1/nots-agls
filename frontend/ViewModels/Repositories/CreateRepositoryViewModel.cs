using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using frontend.Models;
using frontend.Services;
using Microsoft.VisualBasic;

namespace frontend.ViewModels.Repositories;

public interface ICreateRepositoryViewModel
{
    public GitRepoCreateDto Repository { get; set; }
    public RepositoriesCsv RepositoriesCsv { get; set; }
    public Task CreateRepositoryAsync();
    public Task CreateRepositoriesAsync();
}

public class CreateRepositoryViewModel : ICreateRepositoryViewModel
{
    private readonly IRepositoryService _repositoryService;

    public CreateRepositoryViewModel(IRepositoryService repositoryService)
    {
        _repositoryService = repositoryService;
    }
    public GitRepoCreateDto Repository { get; set; } = new();
    public RepositoriesCsv RepositoriesCsv { get; set; } = new();
    public async Task CreateRepositoryAsync()
    {
        await _repositoryService.Create(Repository);
    }

    public async Task CreateRepositoriesAsync()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            Quote = '\''
        };
        using var reader = new StringReader(RepositoriesCsv.csvData);
        using var csv = new CsvReader(reader, config);
        var gitRepos = csv.GetRecords<GitRepoCreateDto>();
        await _repositoryService.Create(gitRepos.ToArray());
    }
}