using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class DataSet : IGenericCrudModel
{
    [Key]
    public int Id { get; set; }
    public string Naam { get; set; }
}

public class DataSetDto
{
    public string Naam { get; set; }
}