using System.ComponentModel.DataAnnotations;

namespace api.Model;
public class GitRepo
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Naam { get; set; }
    [Required]
    public string Url { get; set; }
}

public class GitRepoCreateDto
{
    [Required]
    public string Naam { get; set; }
    [Required]
    public string Url { get; set; }
}