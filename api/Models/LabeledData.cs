using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class LabeledData
{
    [Key]
    public int Id { get; set; }
    public bool IsUseful { get; set; }
    public string Message { get; set; }
    public string? MatchedOnKeyword { get; set; }
    public int DatasetId { get; set; }
    public virtual Dataset Dataset { get; set; }
}

public class LabeledDataDto
{
    public bool IsUseful { get; set; }
    public string Message { get; set; }
    public int DatasetId { get; set; }
    public string? MatchedOnKeyword { get; set; }
}

public class LabeledDataCreateDto
{
    public bool IsUseful { get; set; }
    public string Message { get; set; }
    public int DatasetId { get; set; }
}