using api.Datasets.Models;
using api.Model;
using api.TrainingData;
using Microsoft.ML;

namespace api.Services;

public class MachineLearningService : IMachineLearningService
{
    private readonly ApplicationContext _context;

    private static string? AppPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);

    private readonly MLContext? _mlContext;
    private PredictionEngine<CommitClassification, CommitClassificationPrediction> _predictionEngine;

    public MachineLearningService(ApplicationContext context)
    {
        _context = context;
        _mlContext = new MLContext(0);
    }

    private string GetTrainingDataUploadCachePath(string fileId)
    {
        return Path.Combine(AppPath!, "..", "..", "..", "TrainingData", "UploadCache", $"{fileId}.tsv");
    }

    private string GetTrainedDataModelCachePath(string fileId)
    {
        return Path.Combine(AppPath!, "..", "..", "..", "TrainingData", "ModelCache", $"{fileId}.zip");
    }

    public string LoadTrainingData(string fileName, byte[] fileContent)
    {
        if (!fileName.EndsWith(".tsv")) throw new ArgumentException("Invalid file type/extension, expected '.tsv'");
        var fileId = Guid.NewGuid().ToString("N");
        using var fs = new FileStream(GetTrainingDataUploadCachePath(fileId), FileMode.Create, FileAccess.Write);
        fs.Write(fileContent, 0, fileContent.Length);
        return fileId;
    }

    public bool TrainModel(string fileId, AvailableTrainingSet trainingSet)
    {
        if (File.Exists(GetTrainedDataModelCachePath(fileId))) return false;

        return trainingSet switch
        {
            AvailableTrainingSet.AngularClassification => TrainAngularClassificationStrategy(fileId),
            _ => throw new NotImplementedException()
        };
    }

    public string Predict(string fileId, AvailableTrainingSet trainingSet, string value)
    {
        if (!File.Exists(GetTrainedDataModelCachePath(fileId))) throw new Exception("Model does not exist");

        return trainingSet switch
        {
            AvailableTrainingSet.AngularClassification => PredictAngularClassificationStrategy(fileId, value),
            _ => throw new NotImplementedException()
        };
    }

    private bool TrainAngularClassificationStrategy(string fileId)
    {
        var trainingData = _mlContext!.Data.LoadFromTextFile<CommitClassification>(
            GetTrainingDataUploadCachePath(fileId),
            hasHeader: true
        );

        var pipeline = _mlContext!.Transforms.Conversion
            .MapValueToKey(inputColumnName: "Classification", outputColumnName: "Label")
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "CommitMessage",
                outputColumnName: "CommitMessageFeaturized"))
            .Append(_mlContext.Transforms.Concatenate("Features", "CommitMessageFeaturized"))
            .AppendCacheCheckpoint(_mlContext);

        var trainingPipeline = pipeline
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        var trainedModel = trainingPipeline.Fit(trainingData);
        _predictionEngine = _mlContext.Model
            .CreatePredictionEngine<CommitClassification, CommitClassificationPrediction>(trainedModel);

        _mlContext.Model.Save(trainedModel, trainingData.Schema, GetTrainedDataModelCachePath(fileId));

        return true;
    }

    private string PredictAngularClassificationStrategy(string fileId, string request)
    {
        var loadedModel = _mlContext!.Model.Load(GetTrainedDataModelCachePath(fileId), out _);

        var classification = new CommitClassification
        {
            CommitMessage = request
        };

        _predictionEngine = _mlContext.Model
            .CreatePredictionEngine<CommitClassification, CommitClassificationPrediction>(loadedModel);

        return _predictionEngine.Predict(classification).Classification;
    }
}