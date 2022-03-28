using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class KeywordSetsController: ControllerBase
{
    private readonly IGenericCrudService<KeywordSet> _keywordSetService;

    public KeywordSetsController(IGenericCrudService<KeywordSet> keywordSetService)
    {
        _keywordSetService = keywordSetService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _keywordSetService.Get());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _keywordSetService.Get(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(KeywordSetDto dto)
    {
        var keywordsSet = await _keywordSetService.Create(dto.Adapt<KeywordSet>());
        return Created($"/keywordsets/${keywordsSet.Id}", keywordsSet);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, KeywordSetDto dto)
    {
        var keywordsSet = dto.Adapt<KeywordSet>();
        keywordsSet.Id = id;
        var modified = await _keywordSetService.Update(keywordsSet);
        return Ok(modified);
    }
}