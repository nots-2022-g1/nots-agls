using Microsoft.ML.Data;

namespace api.TrainingData;

public class CommitClassification
{
    [LoadColumn(0)]
    public string ID { get; set; }

    [LoadColumn(1)]
    public string CommitMessage { get; set; }

    [LoadColumn(2)]
    public string Classification { get; set; }
}

public class CommitClassificationPrediction
{
    [ColumnName("PredictedLabel")]
    public string Classification;
}