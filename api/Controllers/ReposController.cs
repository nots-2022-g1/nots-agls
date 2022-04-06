using api.Model;
using api.Parser;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Serilog;

namespace api.Controllers;

public class RepoControllerAttribute : Attribute, IRouteTemplateProvider
{
    public string Template => "repos/{repoId:int}/[controller]";
    public int? Order => 2;
    public string Name { get; set; }
}

[ApiController]
[Route("[controller]")]
public class ReposController : ControllerBase
{
    private readonly IGitRepoService _gitRepoService;
    private readonly IGitCommitService _gitCommitService;
    private readonly GitLogParser _parser;

    public ReposController(IGitRepoService gitRepoService, IGitCommitService gitCommitService)
    {
        _gitRepoService = gitRepoService;
        _gitCommitService = gitCommitService;
        _parser = new GitLogParser();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _gitRepoService.Get());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _gitRepoService.Get(id);
        if (result is null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(GitRepoCreateDto repo)
    {
        // Create Repo
        var gitRepository = await _gitRepoService.Create(repo.Adapt<GitRepo>());

        // Parse all commits
        var parsedCommits = await _parser.Parse(new Uri(repo.Url));
        var adaptedCommits = parsedCommits.Adapt<List<GitCommit>>();
        adaptedCommits.ForEach(item => item.GitRepoId = gitRepository.Id);
        
        await _gitCommitService.Create(adaptedCommits);

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