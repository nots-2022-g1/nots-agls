using System.ComponentModel.DataAnnotations;

namespace frontend.Models;

public class Dataset
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}

public class DatasetDto
{
    public string? Name { get; set; }
}
