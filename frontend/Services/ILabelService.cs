using Refit;
using frontend.Models;

namespace frontend.Services;

public interface ILabelService
{
    [Post("/labels")]
    Task<Label> Create(LabelCreateDto label);

    [Get("/labels")]
    Task<List<Label>> Get();

    [Get("/labels/{id}")]
    Task<Label> GetById(int id);

    [Patch("/labels/{id}")]
    Task<Label> Update(int id, LabelUpdateDto label);

    [Delete("/labels/{id}")]
    Task<Label> Delete(int id);
}
