namespace frontend.Models;
 
public class GitCommit
{
    public string Hash { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public int GitRepoId { get; set; }
}
