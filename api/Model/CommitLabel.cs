namespace api.Model;

public class CommitLabel
{
    public int CommitId { get; set; }
    public int LabelId { get; set; }
    public virtual Commit Commit { get; set; }
    public virtual Label Label { get; set; }
}