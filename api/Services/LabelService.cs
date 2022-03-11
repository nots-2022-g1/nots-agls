using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class LabelService : ILabelService
{
    private readonly ApplicationContext _context;
    private readonly DbSet<Label> _labelRepository;

    public LabelService(ApplicationContext context)
    {
        _context = context;
        _labelRepository = context.Set<Label>();
    }

    public async Task<IList<Label>> Get()
    {
        return await _labelRepository.ToListAsync();
    }

    public async Task<Label> Create(Label label)
    {
        var entity = _labelRepository.Add(label);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<Label> Update(Label repo)
    {
        var entity = _labelRepository.Attach(repo);
        entity.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task Delete(int id)
    {
        _labelRepository.Remove(new Label { Id = id });
        await _context.SaveChangesAsync();
    }
}