namespace frontend.Models;

public class Keyword
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModifiedAt { get; set; }
    public int KeywordSetId { get; set; }
}

public class KeywordDto
{
    public string Name { get; set; }
    public int KeywordSetId { get; set; }
}