namespace frontend.Models;

public class AutoLabelConfig
{
    public int DatasetId { get; set; }
    public int GitRepoId { get; set; }
    public int KeywordSetId { get; set; }
    public bool ExcludeKeyword { get; set; }
}