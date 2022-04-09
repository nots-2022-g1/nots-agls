using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class GitCommit
{
    [Key]
    public string Hash { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public int GitRepoId { get; set; }
    public virtual GitRepo GitRepo { get; set; }
}

public class GitCommitResponseDto
{
    public string Hash { get; set; }
    public DateTime Date { get; set; }
    public string Message { get; set; }
    public int GitRepoId { get; set; }
}

public class GitLogParserGitCommit
{
    public string commit_date { get; set; }
    public string commit_hash { get; set; }
    public string message { get; set; }
}