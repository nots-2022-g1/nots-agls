using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Net;
using api.Models;
using api.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace api.Controllers;

public class DataSetControllerAttribute : Attribute, IRouteTemplateProvider
{
    public string Template => "datasets/{dataSetId:int}/[controller]";
    public int? Order => 2;
    public string Name { get; set; }
}

[ApiController]
[Route("[controller]")]
public class DataSetsController : GenericCrudController<Dataset, DataSetDto>
{
    private readonly ILabeledDataService _labeledDataService;
    private readonly IGitCommitService _gitCommitService;
    private readonly IKeywordService _keywordService;

    public DataSetsController(
        IGenericCrudService<Dataset> service,
        ILabeledDataService labeledDataService,
        IGitCommitService gitCommitService,
        IKeywordService keywordService
    ) : base(service)
    {
        _labeledDataService = labeledDataService;
        _gitCommitService = gitCommitService;
        _keywordService = keywordService;
    }

    public override async Task<IActionResult> Post(DataSetDto dto)
    {
        var created = await _service.Create(dto.Adapt<Dataset>());
        created.CreatedAt = created.LastModifiedAt = DateTime.UtcNow;
        return Created($"/datasets/{created.Id}", created);
    }

    public override async Task<IActionResult> Put(int id, DataSetDto dto)
    {
        var dataSet = dto.Adapt<Dataset>();
        dataSet.Id = id;
        dataSet.LastModifiedAt = DateTime.UtcNow;
        var modified = await _service.Update(dataSet);
        return Ok(modified);
    }

    [HttpPost("autolabel")]
    public async Task<IActionResult> AutoLabel(AutoLabelConfig config)
    {
        var commits = await _gitCommitService.Get(config.GitRepoId);
        var keywords = await _keywordService.GetByKeywordSetId(config.KeywordSetId);
        var concurrentBag = new ConcurrentBag<LabeledData>();

        Parallel.ForEach(commits, commit => { concurrentBag.Add(LabelCommit(commit, keywords, config)); });

        ICollection<LabeledData> labeledData = concurrentBag.ToArray();

        if (config.BalanceOutput)
        {
            var usefuls = labeledData.Where(e => e.IsUseful.Equals(true));
            var notUsefuls = labeledData.Where(e => e.IsUseful.Equals(false));
            var labeledDatas = usefuls as LabeledData[] ?? usefuls.ToArray();
            labeledData = labeledDatas.Concat(notUsefuls.Take(labeledDatas.Length)).ToList();
        }
        await _labeledDataService.Add(labeledData);
        return Ok();
    }

    private static LabeledData LabelCommit(GitCommit commit, ICollection<Keyword> keywords, AutoLabelConfig config)
    {
        var tokens = commit.Message.Split(' ');
        var labeledData = new LabeledData
        {
            Message = commit.Message,
            DatasetId = config.DatasetId,
            IsUseful = false
        };

        foreach (var token in tokens)
        {
            foreach (var keyword in keywords)
            {
                if (!keyword.Name.Equals(token, StringComparison.InvariantCultureIgnoreCase)) continue;
                labeledData.IsUseful = true;
                labeledData.MatchedOnKeyword = keyword.Name;
                if (config.ExcludeKeyword)
                {
                    labeledData.Message = string.Join(' ', tokens.Where(t => !t.Equals(token)));
                }
                break;
            }
        }

        return labeledData;
    }
}