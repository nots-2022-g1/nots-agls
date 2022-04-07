namespace frontend.Models;

public class UploadTrainingDataDto
{
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
}

public class TrainModelDto
{
    public string FileId { get; set; }
    public int TrainingSet { get; set; }
}

public class PredictDto
{
    public string FileId { get; set; }
    public int TrainingSet { get; set; }
    public string Value { get; set; }
}