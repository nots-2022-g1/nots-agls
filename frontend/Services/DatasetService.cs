using System.Globalization;
using System.Text;
using CsvHelper;
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

    public async Task<string> GenerateTsvAsync(int id)
    {
        var labeledData = await _client.GetLabeledData(id);

        await using TextWriter writer = new StringWriter();
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(labeledData);

        return writer.ToString() ?? throw new InvalidOperationException();
    }

    public Task AutoLabel(AutoLabelConfig config)
    {
        return _client.AutoLabel(config);
    }
}