using System.ComponentModel.DataAnnotations;

namespace frontend.Models;

public class Label
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class LabelCreateDto
{
    [Required]
    public string Name { get; set; } = default!;
}

public class LabelUpdateDto
{
    public string Name { get; set; } = default!;
}