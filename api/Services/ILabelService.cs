using api.Model;

namespace api.Services;

public interface ILabelService
{
    Task<IList<Label>> Get();
    Task<Label> Create(Label label);
    Task<Label> Update(Label label);
    Task Delete(int id);
}