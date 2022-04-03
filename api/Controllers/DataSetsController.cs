using System.Net;
using api.Model;
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

    public DataSetsController(IGenericCrudService<Dataset> service, ILabeledDataService labeledDataService,
        IGitCommitService gitCommitService, IKeywordService keywordService) :
        base(service)
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
        var autoLabeledData = new List<LabeledData>();
        
        foreach (var commit in commits)
        {
            var labeledData = new LabeledData
            {
                GitCommitHash = commit.Hash,
                DatasetId = config.DatasetId,
                IsUseful = false
            };
            foreach (var keyword in keywords)
            {
                if (commit.Message.Contains(keyword.Name))
                {
                    labeledData.IsUseful = true;
                }
            }
            autoLabeledData.Add(labeledData);
        }

        await _labeledDataService.Add(autoLabeledData);
        return Ok();
    }
}