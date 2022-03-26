namespace api.Model;

public class GenericCrudModel : IGenericCrudModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
}

public class GenericCrudModelDto
{
    public string Name { get; set; }
}