using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace api.Controllers;

public class DataSetControllerAttribute : Attribute, IRouteTemplateProvider
{
    public string Template => "datasets/{dataSetId:int}/[controller]";
    public int? Order => 2;
    public string Name { get; set; }
}

[ApiController]
[Route("[controller]")]
public class DataSetsController : GenericCrudController<DataSet, DataSetDto>
{
    private readonly ILabeledDataService _dataService;

    public DataSetsController(IGenericCrudService<DataSet> service, ILabeledDataService dataService) :
        base(service)
    {
        _dataService = dataService;
    }

    public override async Task<IActionResult> Post(DataSetDto dto)
    {
        var created = await _service.Create(dto.Adapt<DataSet>());
        created.CreatedAt = created.LastModifiedAt = DateTime.UtcNow;
        return Created($"/datasets/{created.Id}", created);
    }

    public override async Task<IActionResult> Put(int id, DataSetDto dto)
    {
        var dataSet = dto.Adapt<DataSet>();
        dataSet.Id = id;
        dataSet.LastModifiedAt = DateTime.UtcNow;
        var modified = await _service.Update(dataSet);
        return Ok(modified);
    }

    

}