using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class LabelController : ControllerBase
{
    private readonly ILabelService _labelService;

    public LabelController(ILabelService labelService)
    {
        _labelService = labelService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _labelService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Post(LabelCreateDto label)
    {
        var createdLabel = await _labelService.Create(label.Adapt<Label>());
        return Created($"/labels/${createdLabel.Id}", createdLabel);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, Label label)
    {
        label.Id = id;
        var modifiedLabel = await _labelService.Update(label);
        return Ok(modifiedLabel);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _labelService.Delete(id);
        return Ok();
    }
}