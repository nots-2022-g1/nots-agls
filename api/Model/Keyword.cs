using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class Keyword
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public int KeywordsetId { get; set; }
    public virtual KeywordsSet KeywordsSet { get; set; }
}

public class KeywordDto
{
    public string Name { get; set; }
}
