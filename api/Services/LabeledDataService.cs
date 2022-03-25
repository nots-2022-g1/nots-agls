using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class LabeledDataService : ILabeledDataService
{
    private readonly ApplicationContext _context;
    private readonly DbSet<LabeledData> _dataRepository;

    public LabeledDataService(ApplicationContext context)
    {
        _context = context;
        _dataRepository = context.Set<LabeledData>();
    }

    public async Task<IList<LabeledData>> Get(int dataSetId)
    {
        return await _dataRepository.Where(e => e.DataSetId.Equals(dataSetId)).ToListAsync();
    }

    public async Task<LabeledData> Create(LabeledData data)
    {
        var created = _dataRepository.Add(data);
        await _context.SaveChangesAsync();
        return created.Entity;
    }

    public async Task<LabeledData> Update(LabeledData data)
    {
        var entity = _dataRepository.Attach(data);
        entity.State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task Delete(int id)
    {
        _dataRepository.Remove(new LabeledData { Id = id });
        await _context.SaveChangesAsync();
    }
}