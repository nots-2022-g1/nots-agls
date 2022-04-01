using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("KeywordSets/{keywordSetId:int}/[controller]")]
public class KeywordsController: ControllerBase
{
    private readonly IKeywordService _keywordService;

    public KeywordsController(IKeywordService keywordService)
    {
        _keywordService = keywordService;
    }

    [HttpGet]
    public async Task<IActionResult> GetKeywords(int keywordSetId)
    {
        var response = await _keywordService.GetByKeywordSetId(keywordSetId);
        return Ok(response.Adapt<IList<KeywordResponseDto>>());
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(int keywordSetId, KeywordDto keyword)
    {
        keyword.KeywordSetId = keywordSetId;
        var createdKeyword = await _keywordService.Create(keyword.Adapt<Keyword>());
        return Created($"/keywordset/{keywordSetId}/{createdKeyword.Id}", createdKeyword);
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int keywordSetId, int id, KeywordDto keyword)
    {
        keyword.KeywordSetId = id;
        var createdKeyword = await _keywordService.Update(keyword.Adapt<Keyword>());
        return Created($"/keywordset/{id}/{createdKeyword.Id}", createdKeyword);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int keywordSetId, int id)
    { 
        await _keywordService.Delete(id);
        return Ok();
    }
}