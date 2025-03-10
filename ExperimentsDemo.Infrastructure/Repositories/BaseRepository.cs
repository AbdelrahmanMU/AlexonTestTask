using ExperimentsDemo.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExperimentsDemo.Infrastructure.Repositories;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : class
{
    private readonly ApplicationDbContext _context = context;

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> GetAll(string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes is not null)
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

        return query;
    }

    public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken, string[]? includes = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes is not null)
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

        return await query.FirstOrDefaultAsync(entity => EF.Property<Guid>(entity, "Id") == id, cancellationToken);
    }

    public T Update(T entity)
    {
        _context.Set<T>().Update(entity);

        return entity;
    }
}
