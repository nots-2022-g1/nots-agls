using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Keyword : IGenericCrudModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public int KeywordSetId { get; set; }
    public virtual KeywordSet KeywordSet { get; set; }
}

public class KeywordDto
{
    public string Name { get; set; }
    public int KeywordSetId { get; set; }
}

public class KeywordResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int KeywordSetId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}
