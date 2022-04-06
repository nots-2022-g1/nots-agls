namespace api.Services;

public interface IMachineLearningService
{
    public bool LoadAndTrainDataset(bool saveToFile = false);
    public string PredictClassification(string commitMessage);
}