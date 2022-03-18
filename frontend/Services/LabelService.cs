using Refit;
using frontend.Models;

namespace frontend.Services;

public class LabelService : ILabelService
{
    private readonly ILabelService _client;

    public LabelService(IConfiguration config, HttpClient httpClient)
    {

        httpClient.BaseAddress = new Uri(config.GetSection("MyAppSettings").GetValue<string>("ApiUrl"));
        this._client = RestService.For<ILabelService>(httpClient, new RefitSettings());
    }

    public async Task<Label> Create(LabelCreateDto label)
    {
        return await this._client.Create(label);
    }

    public async Task<List<Label>> Get()
    {
        return await this._client.Get();
    }

    public async Task<Label> GetById(int id)
    {
        return await this._client.GetById(id);
    }

    public async Task<Label> Update(int id, LabelUpdateDto label)
    {
        return await this._client.Update(id, label);
    }

    public async Task<Label> Delete(int id)
    {
        return await this._client.Delete(id);
    }
}
