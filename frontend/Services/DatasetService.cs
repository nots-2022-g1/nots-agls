using System.Text;
using Refit;
using frontend.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace frontend.Services;

public class DatasetService : IDatasetService
{
    private readonly IDatasetService _client;

    public DatasetService(IConfiguration config, HttpClient httpClient)
    {

        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        _client = RestService.For<IDatasetService>(httpClient, new RefitSettings());
    }

    public async Task<ApiResponse<Dataset>> Create(DatasetDto dataset)
    {
        Log.Information("TEST");
        Log.Information(dataset.Name);
        return await _client.Create(dataset);
    }

    public async Task<List<Dataset>> Get()
    {
        return await _client.Get();
    }

    public async Task<Dataset> GetById(int id)
    {
        return await _client.GetById(id);
    }

    public async Task<List<LabeledData>> GetLabeledData(int id)
    {
        return await _client.GetLabeledData(id);
    }

    public async Task<ApiResponse<Dataset>> Update(int id, DatasetDto dataset)
    {
        return await _client.Update(id, dataset);
    }

    public async Task<ApiResponse<Dataset>> Delete(int id)
    {
        return await _client.Delete(id);
    }

    public async Task<string> GenerateCsvAsync(int id)
    {
        var response = await _client.GetLabeledData(id);
        
        var columns = new List<string>() {"GitCommitMessage", "IsUseful", "MatchedOnKeyword"};
        
        var csvFile = string.Join(",", columns);
        
        foreach (var labeledData in response)
        {
            var csvRow = Environment.NewLine;
            csvRow += $"{labeledData.GitCommit.Message},{labeledData.IsUseful},null";
            csvFile += csvRow;
        }

        return csvFile;
    }
}
