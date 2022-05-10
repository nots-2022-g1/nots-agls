using api.Models;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using Mapster;

namespace api.Controllers;

[ApiController]
[DataSetController]
public class LabeledDataController : ControllerBase
{
    
    private readonly ILabeledDataService _labeledDataService;

    public LabeledDataController(ILabeledDataService labeledDataService)
    {
        _labeledDataService = labeledDataService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int dataSetId, int id)
    {
        var result = await _labeledDataService.Get(dataSetId, id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(int dataSetId)
    {
        var result = await _labeledDataService.Get(dataSetId);
        return Ok(result.Adapt<IList<LabeledDataDto>>());
    }

    [HttpPost]
    public async Task<IActionResult> PostData(int dataSetId, LabeledDataCreateDto data)
    {
        var created = await _labeledDataService.Create(data.Adapt<LabeledData>());
        return Created($"/datasets/{dataSetId}/data/{created.Id}", created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutData(int dataSetId, int id, LabeledDataCreateDto data)
    {
        data.DatasetId = dataSetId;
        var labeledData = data.Adapt<LabeledData>();
        labeledData.Id = id;
        var modified = await _labeledDataService.Update(labeledData);
        return Ok(modified);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteData(int dataSetId, int id)
    {
        await _labeledDataService.Delete(dataSetId, id);
        return Ok();
    }
}