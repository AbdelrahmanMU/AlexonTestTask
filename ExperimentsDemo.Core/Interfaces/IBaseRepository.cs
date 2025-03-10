namespace ExperimentsDemo.Core.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, string[]? includes = null);
    IQueryable<T> GetAll(string[]? includes = null);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
    T Update(T entity);
    void Delete(T entity);
}
