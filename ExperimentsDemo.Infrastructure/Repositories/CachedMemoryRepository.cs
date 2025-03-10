using ExperimentsDemo.Core.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ExperimentsDemo.Infrastructure.Repositories;

public class CachedMemoryRepository<T>(BaseRepository<T> decorated,
    IMemoryCache memoryCache,
    IDistributedCache distributedCache) : IBaseRepository<T> where T : class
{
    private readonly BaseRepository<T> _decorated = decorated;
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly IDistributedCache _distributedCache = distributedCache;
    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
    {
        return _decorated.CreateAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        _decorated.Delete(entity);
    }

    public IQueryable<T> GetAll(string[]? includes = null)
    {
        return _decorated.GetAll(includes);
    }

    //public Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken, string[]? includes = null)
    //{
    //    var key = $"member-{id}";

    //    return _memoryCache.GetOrCreate(key,
    //        entry =>
    //        {
    //            return _decorated.GetByIdAsync(id, cancellationToken, includes);
    //        });
    //}

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, string[]? includes = null)
    {
        var key = $"member-{id}";

        string cachedMember = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrWhiteSpace(cachedMember))
        {
            var item = await _decorated.GetByIdAsync(id, cancellationToken);

            if (item is null)
            {
                return item;
            }

            var serializedMember = JsonConvert.SerializeObject(item);

            await _distributedCache.SetStringAsync(key,
                serializedMember,
                cancellationToken);

            return item;
        }

        var deserializedItem = JsonConvert.DeserializeObject<T>(cachedMember);

        return deserializedItem;
    }

    public T Update(T entity)
    {
        return _decorated.Update(entity);
    }
}
