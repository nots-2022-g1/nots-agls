namespace frontend.Models;

public class LabeledData
{
    public bool IsUseful { get; set; }
    public string Message { get; set; }
    public int DatasetId { get; set; }
    public string? MatchedOnKeyword { get; set; }
}