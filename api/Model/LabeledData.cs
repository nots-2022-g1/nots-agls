using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class LabeledData
{
    [Key]
    public int Id { get; set; }
    public bool IsUseful { get; set; }
    public string GitCommitHash { get; set; }
    public int DatasetId { get; set; }
    public virtual GitCommit GitCommit { get; set; }
    public virtual Dataset Dataset { get; set; }
}

public class LabeledDataDto
{
    public bool IsUseful { get; set; }
    public GitCommitResponseDto GitCommit { get; set; }
    public int DatasetId { get; set; }
}

public class LabeledDataCreateDto
{
    public bool IsUseful { get; set; }
    public string GitCommitHash { get; set; }
    public int DatasetId { get; set; }
}