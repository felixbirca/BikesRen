namespace BikesRent.BusinessLogicLayer;

using Microsoft.Extensions.Caching.Memory;
using System;
using System.IO;
using System.Text.Json;

public class MemoryCacheService : ICache
{
    
    private readonly IMemoryCache _cache;
    private readonly MemoryCacheEntryOptions _cacheOptions;
    private List<string> _keys; 

    public MemoryCacheService(IMemoryCache cache, int cacheTime = 60)
    {
        _cache = cache;
        _cacheOptions = new MemoryCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime)
        };
        _keys = new List<string>();
    }

    public T Get<T>(string key)
    {
        if (_cache.TryGetValue(key, out byte[] data))
        {
            return JsonSerializer.Deserialize<T>(data);
        }
        return default;
    }

    public void Set<T>(string key, T value, int? cacheTime = null)
    {
        var options = _cacheOptions;

        if (cacheTime.HasValue)
        {
            options = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(cacheTime.Value)
            };
        }

        byte[] data = JsonSerializer.SerializeToUtf8Bytes(value);
        _cache.Set(key, data, options);
    }

    public bool IsSet(string key)
    {
        return _cache.TryGetValue(key, out _);
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    // public void RemoveByPattern(string pattern)
    // {
    //     
    // }
    //
    // public void Clear()
    // {
    //     // Clearing all cache data directly is not supported by IMemoryCache.
    //     // You would need to track keys manually if you need to clear the cache.
    // }
}

