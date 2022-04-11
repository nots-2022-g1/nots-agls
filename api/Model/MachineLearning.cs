using System.Runtime.Serialization;
using api.TrainingData;

namespace api.Model;

public class LoadTrainingDataDto
{
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
}

public class TrainModelDto
{
    public string FileId { get; set; }
    public AvailableTrainingSet TrainingSet { get; set; }
}

public class PredictDto
{
    public string FileId { get; set; }
    public AvailableTrainingSet TrainingSet { get; set; }
    public string Value { get; set; }
}