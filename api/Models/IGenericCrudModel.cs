using System.ComponentModel.DataAnnotations;

namespace api.Model;

public interface IGenericCrudModel
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}