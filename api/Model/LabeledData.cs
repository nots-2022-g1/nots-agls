using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class LabeledData
{
    [Key]
    public int Id { get; set; }
    public bool IsUseful { get; set; }
    public string GitCommitHash { get; set; }
    public int DataSetId { get; set; }
    public virtual GitCommit GitCommit { get; set; }
    public virtual DataSet DataSet { get; set; }
}

public class LabeledDataDto
{
    public bool IsUseful { get; set; }
    public string GitCommitHash { get; set; }
    public int DataSetId { get; set; }
}