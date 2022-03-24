namespace api.Model;

public class LabeledData
{
    public int Id { get; set; }
    public bool UseFul { get; set; }
    public int GitCommitId { get; set; }
    public int DataSetId { get; set; }
    public virtual GitCommit GitCommit { get; set; }
    public virtual DataSet DataSet { get; set; }
}