using api.Models;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[RepoController]
[ApiController]
public class CommitsController : ControllerBase
{
    private readonly IGitCommitService _gitCommitService;

    public CommitsController(IGitCommitService gitCommitService)
    {
        _gitCommitService = gitCommitService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int repoId)
    {
        var results = await _gitCommitService.Get(repoId);
        return Ok(results.Adapt<IList<GitCommitResponseDto>>());
    }

    [HttpPost]
    public async Task<IActionResult> Post(int repoId, IList<GitLogParserGitCommit> gitLogParserCommits)
    {
        var gitCommits = gitLogParserCommits.Adapt<IList<GitCommit>>();
        foreach (var commit in gitCommits)
        {
            commit.GitRepoId = repoId;
        }
        await _gitCommitService.Create(gitCommits);
        return Created($"/repos/{repoId}/commits", null);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _gitCommitService.Delete(id);
        return Ok();
    }
}