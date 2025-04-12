using System;

namespace TaskManagement_RESTAPI.Services.Contracts;

public interface ICacheService
{
    Task<T?> GetCacheAsync<T>(string key) ;
    Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null);
    Task RemoveCacheAsync<T>(string key);
    Task AddKeyToSetAsync(string key, string cacheKey);
    Task InvalidateAllKeysInSet(string key);
}
