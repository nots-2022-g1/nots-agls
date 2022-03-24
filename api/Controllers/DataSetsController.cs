using api.Model;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class DataSetsController : GenericCrudController<DataSet>
{
    private readonly ILabeledDataService _dataService;
    
    public DataSetsController(IGenericCrudService<DataSet> service, ILabeledDataService dataService) :
        base(service)
    {
        _dataService = dataService;
    }

    public override Task<IActionResult> Post(DataSet entity)
    {
        throw new NotImplementedException();
    }

    public override Task<IActionResult> Put(int id, DataSet entity)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:int}/data")]
    public async Task<IActionResult> GetData(int id)
    {
        return Ok(await _dataService.Get(id));
    }
}