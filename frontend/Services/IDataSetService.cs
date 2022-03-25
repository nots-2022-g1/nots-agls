using frontend.Models;
using Refit;

namespace frontend.Services;

public interface IDataSetService
{
    [Get("/datasets")]
    Task<List<DataSet>> Get();

    [Get("/datasets/{id}")]
    Task<DataSet> GetById(int id);
    
    [Post("/datasets")]
    Task Create(DataSet dataSet);

    [Put("/datasets/{id}")]
    Task Update(int id, DataSet dataSet);

    [Delete("/datasets/{id}")]
    Task Delete(int id);
}