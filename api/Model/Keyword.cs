using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class Keyword
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

public class KeywordCreateDto
{
    public string Name { get; set; }
}

public class KeywordUpdateDto
{
    public string Name { get; set; }
}
