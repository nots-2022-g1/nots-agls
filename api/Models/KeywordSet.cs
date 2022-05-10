using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class KeywordSet : IGenericCrudModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}

public class KeywordSetDto
{
    public string Name { get; set; }
}