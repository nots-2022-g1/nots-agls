using System.ComponentModel.DataAnnotations;

namespace api.Model;


public class Commit
{
    [Key]
    public string Hash { get; set; }
    public string Message { get; set; }
    public virtual GitRepository GitRepository { get; set; }
}