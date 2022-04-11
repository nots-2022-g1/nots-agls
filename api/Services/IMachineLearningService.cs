using api.TrainingData;

namespace api.Services;

public interface IMachineLearningService
{
    public string LoadTrainingData(string fileName, byte[] fileContent);
    public bool TrainModel(string fileId, AvailableTrainingSet trainingSet);
    public string Predict(string fileId, AvailableTrainingSet trainingSet, string value);
}