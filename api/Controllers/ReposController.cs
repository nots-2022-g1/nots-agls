using api.Model;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReposController : ControllerBase
{
    private readonly IGitRepoService _gitRepoService;

    public ReposController(IGitRepoService gitRepoService)
    {
        _gitRepoService = gitRepoService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _gitRepoService.Get());
    }

    [HttpPost]
    public async Task<IActionResult> Post(GitRepoCreateDto repo)
    {
        var gitRepository = await _gitRepoService.Create(repo.Adapt<GitRepo>());
        return Created($"/repos/${gitRepository.Id}", gitRepository);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, GitRepo repo)
    {
        repo.Id = id;
        var modified = await _gitRepoService.Update(repo);
        return Ok(modified);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _gitRepoService.Delete(id);
        return Ok();
    }
}