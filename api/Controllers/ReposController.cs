using System.Collections.ObjectModel;
using api.Models;
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
    private readonly IGitService _gitService;
    private readonly IGitCommitService _gitCommitService;
    private readonly GitLogParser _parser;

    public ReposController(IGitRepoService gitRepoService, IGitCommitService gitCommitService, GitLogParser parser,
        IGitService gitService)
    {
        _gitRepoService = gitRepoService;
        _gitCommitService = gitCommitService;
        _parser = parser;
        _gitService = gitService;
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

    private async Task LogAndParseRepo(GitRepo repo)
    {
        var gitlog = await _gitService.Log(repo);

        var commits = new Collection<GitCommit>();
        await foreach (var commit in GitLogParser.ParseGitCommitsAsync(gitlog, repo))
        {
            commits.Add(commit);
        }

        try
        {
            await _gitCommitService.Create(commits);
        }
        catch (Exception)
        {
            Log.Error("error adding commits to the database");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(GitRepoCreateDto repo)
    {
        var gitRepository = await _gitRepoService.Create(repo.Adapt<GitRepo>());

        await LogAndParseRepo(gitRepository);

        return Created($"/repos/${gitRepository.Id}", gitRepository);
    }
    
    [HttpPost("multi")]
    public async Task<IActionResult> PostMulti(ICollection<GitRepoCreateDto> repos)
    {
        foreach (var repo in repos)
        {
            var gitRepository = await _gitRepoService.Create(repo.Adapt<GitRepo>());
            await LogAndParseRepo(gitRepository);
        }

        return StatusCode(StatusCodes.Status201Created);
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