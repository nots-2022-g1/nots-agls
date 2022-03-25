namespace frontend.Models;

public class LabeledData
{
    public int Id { get; set; }
    public string GitCommitHash { get; set; }
    public bool IsUseful { get; set; }
}