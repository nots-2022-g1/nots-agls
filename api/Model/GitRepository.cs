using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class GitRepository
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Naam { get; set; }
    [Required]
    public string Url { get; set; }
}