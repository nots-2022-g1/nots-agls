namespace api.Services;

public interface IGenericCrudService<T>
{
    Task<T?> Get(int id);
    Task<IList<T>> Get();
    Task<T> Create(T entity);
    Task<T> Update(T entity);
    Task Delete(int id);
}