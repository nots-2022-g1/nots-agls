namespace api.Models;

public class GitLogCommit
{
    public string? Hash { get; set; }
    public string? Author { get; set; }
    public DateTime? Date { get; set; }
    public string? Message { get; set; }
    public bool IsMerge { get; set; }
}