using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class Dataset : IGenericCrudModel
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}

public class DataSetDto
{
    public string Name { get; set; }
}