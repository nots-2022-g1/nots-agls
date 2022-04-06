using api.Datasets.Models;
using api.Model;
using Microsoft.ML;

namespace api.Services;

public class MachineLearningService : IMachineLearningService
{
    private readonly ApplicationContext _context;

    private static string? AppPath => Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
    private static string TrainDataPath => Path.Combine(AppPath!, "..", "..", "..", "Datasets", "training_angular-commits.tsv");
    private static string ModelPath => Path.Combine(AppPath!, "..", "..", "..", "TrainedModels", "angular-model.zip");

    private static MLContext? _mlContext;
    private static PredictionEngine<CommitClassification, CommitClassificationPrediction> _predictionEngine;
    private static ITransformer _trainedModel;
    private static IDataView _trainingDataView;

    public MachineLearningService(ApplicationContext context)
    {
        _context = context;
        _mlContext = new MLContext(0);
        _trainingDataView = _mlContext.Data.LoadFromTextFile<CommitClassification>(TrainDataPath, hasHeader: true);
    }

    public bool LoadAndTrainDataset(bool saveToFile = false)
    {
        if (File.Exists(ModelPath))
        {
            return false;
        }
        
        var pipeline = _mlContext!.Transforms.Conversion
            .MapValueToKey(inputColumnName: "Classification", outputColumnName: "Label")
            .Append(_mlContext.Transforms.Text.FeaturizeText(inputColumnName: "CommitMessage",
                outputColumnName: "CommitMessageFeaturized"))
            .Append(_mlContext.Transforms.Concatenate("Features", "CommitMessageFeaturized"))
            .AppendCacheCheckpoint(_mlContext);

        var trainingPipeline = pipeline
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _trainedModel = trainingPipeline.Fit(_trainingDataView);
        _predictionEngine = _mlContext.Model
            .CreatePredictionEngine<CommitClassification, CommitClassificationPrediction>(_trainedModel);

        if (saveToFile)
        {
            _mlContext.Model.Save(_trainedModel, _trainingDataView.Schema, ModelPath);
        }

        return true;
    }

    public string PredictClassification(string commitMessage)
    {
        ITransformer loadedModel = _mlContext.Model.Load(ModelPath, out _);

        CommitClassification classification = new CommitClassification
        {
            CommitMessage = commitMessage
        };

        _predictionEngine = _mlContext.Model
            .CreatePredictionEngine<CommitClassification, CommitClassificationPrediction>(loadedModel);

        return _predictionEngine.Predict(classification).Classification;
    }
}