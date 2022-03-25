using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

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
        return Created($"/datasets/{created.Id}", created);
    }

    public override async Task<IActionResult> Put(int id, DataSetDto dto)
    {
        var dataSet = dto.Adapt<DataSet>();
        dataSet.Id = id;
        var modified = await _service.Update(dataSet);
        return Ok(modified);
    }

    [HttpGet("{id:int}/data")]
    public async Task<IActionResult> GetData(int id)
    {
        return Ok(await _dataService.Get(id));
    }

    [HttpPost("{id:int}/data")]
    public async Task<IActionResult> PostData(int id, LabeledDataDto data)
    {
        var createdLabel = await _dataService.Create(data.Adapt<LabeledData>());
        return Created($"/datasets/{id}/data/{createdLabel.Id}", createdLabel);
    }

    [HttpPut("{dataSetId:int}/data/{id:int}")]
    public async Task<IActionResult> PutData(int dataSetId, int id, LabeledDataDto data)
    {
        data.DataSetId = dataSetId;
        var labeledData = data.Adapt<LabeledData>();
        labeledData.Id = id;
        var modified = await _dataService.Update(labeledData);
        return Ok(modified);
    }

    [HttpDelete("{dataSetId:int}/data/{id:int}")]
    public async Task<IActionResult> DeleteData(int dataSetId, int id)
    {
        await _dataService.Delete(id);
        return Ok();
    }

}