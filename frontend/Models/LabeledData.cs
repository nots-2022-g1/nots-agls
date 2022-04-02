namespace frontend.Models;

public class LabeledData
{
    public bool IsUseful { get; set; }
    public GitCommit GitCommit { get; set; }
    public int DatasetId { get; set; }
}