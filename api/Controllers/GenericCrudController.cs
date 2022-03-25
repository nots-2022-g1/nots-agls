using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class GenericCrudController<T, U> : ControllerBase where T : class
{
    protected readonly IGenericCrudService<T> _service;

    protected GenericCrudController(IGenericCrudService<T> service)
    {
        _service = service;
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _service.Get(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _service.Get());
    }

    [HttpPost]
    public abstract Task<IActionResult> Post(U dto);

    [HttpPut("{id:int}")]
    public abstract Task<IActionResult> Put(int id, U dto);

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.Delete(id);
        return Ok();
    }

}