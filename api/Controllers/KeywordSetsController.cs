using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

public class KeywordSetsController : GenericCrudController<GenericCrudModel, GenericCrudModelDto>
{
    private readonly IGenericCrudService<Keyword> _keywordService;
    private readonly IKeywordsSetService _keywordsSetService;

    public KeywordSetsController(IKeywordsSetService keywordsSetService, IGenericCrudService<Keyword> keywordService) : base(keywordsSetService)
    {
        _keywordsSetService = keywordsSetService;
        _keywordService = keywordService;
    }

    public override async Task<IActionResult> Post(GenericCrudModelDto dto)
    {
        var keywordsSet = await _service.Create(dto.Adapt<GenericCrudModel>());
        return Created($"/keywordssets/${keywordsSet.Id}", keywordsSet);
    }

    public override async Task<IActionResult> Put(int id, GenericCrudModelDto dto)
    {
        var keywordsSet = dto.Adapt<GenericCrudModel>();
        keywordsSet.Id = id;
        var modified = await _service.Update(keywordsSet);
        return Ok(modified);
    }

    [HttpGet("{id:int}/keywords")]
    public async Task<IActionResult> GetKeywords(int id)
    {
        return Ok(await _keywordsSetService.GetKeywords(id));
    }
    
    [HttpPost("{id:int}/keywords")]
    public async Task<IActionResult> Post(int id, KeywordDto keyword)
    {
        keyword.KeywordsetId = id;
        var createdKeyword = await _keywordService.Create(keyword.Adapt<Keyword>());
        return Created($"/keywordset/{id}/{createdKeyword.Id}", createdKeyword);
    }
    
    [HttpPut("{id:int}/keywords/{kwId:int}")]
    public async Task<IActionResult> Put(int id, int kwId, KeywordDto keyword)
    {
        keyword.KeywordsetId = id;
        var createdKeyword = await _keywordService.Update(keyword.Adapt<Keyword>());
        return Created($"/keywordset/{id}/{createdKeyword.Id}", createdKeyword);
    }
    
    [HttpDelete("{id:int}/keywords/{kwId:int}")]
    public async Task<IActionResult> Delete(int id, int kwId)
    { 
        await _keywordService.Delete(kwId);
        return Ok();
    }
}