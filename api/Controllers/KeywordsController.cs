using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class KeywordsController : ControllerBase
{
    private readonly IKeywordService _keywordService;

    public KeywordsController(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _keywordService.Get());
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _keywordService.Get(id);
        if (result is null) return NotFound();

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> Post(KeywordCreateDto keyword)
    {
        var createdKeyword = await _keywordService.Create(keyword.Adapt<Keyword>());
        return Created($"/keywords/${createdKeyword.Id}", createdKeyword);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Patch(int id, KeywordUpdateDto keyword)
    {

        var _keyword = keyword.Adapt<Keyword>();
        _keyword.Id = id;

        var modifiedKeyword = await _keywordService.Update(_keyword);
        return Ok(modifiedKeyword);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _keywordService.Delete(id);
        return Ok();
    }
}