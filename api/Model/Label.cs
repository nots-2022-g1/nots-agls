using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class Label
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

public class LabelCreateDto
{
    [Required]
    public string Name { get; set; }
}