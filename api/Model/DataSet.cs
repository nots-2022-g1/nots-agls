using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class DataSet
{
    [Key]
    public int Id { get; set; }
    public string Naam { get; set; }
}