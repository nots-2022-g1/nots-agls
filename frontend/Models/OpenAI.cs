public class OpenAI
{
    public string id { get; set; }
    public int created { get; set; }
    public string model { get; set; }
    public List<Choice> choices { get; set; }
}

public class Choice
{
    public string text { get; set; }
    public int index { get; set; }
    public string finish_reason { get; set; }
}

public class OpenAISummarizeDTO
{
    public string prompt { get; set; }
    public double temperature { get; set; } = 0.7;
    public int max_tokens { get; set; } = 60;
    public double top_p { get; set; } = 1.0;
    public double frequency_penalty { get; set; } = 0.0;
    public double presence_penalty { get; set; } = 0.0;
}