using System.ComponentModel.DataAnnotations;

namespace api.Model;

public class Keyword : GenericCrudModel
{
    public int KeywordsetId { get; set; }
    public virtual GenericCrudModel KeywordsSet { get; set; }
}

public class KeywordDto : GenericCrudModelDto
{
    public int KeywordsetId { get; set; }
}
