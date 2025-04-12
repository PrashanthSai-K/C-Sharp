using System;
using System.Data.Common;
using System.Text.Json;
using StackExchange.Redis;
using TaskManagement_RESTAPI.Services.Contracts;

namespace TaskManagement_RESTAPI.Services.Concrete;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }

    public async Task RemoveCacheAsync<T>(string key)
    {
        await _database.KeyDeleteAsync(key);
    }

    public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await _database.StringSetAsync(key, jsonData, expiry);
    }


    public async Task<T?> GetCacheAsync<T>(string key)
    {
        var data = await _database.StringGetAsync(key);
        if (data.IsNullOrEmpty) return default;
        return JsonSerializer.Deserialize<T>(data!);
    }

    public async Task AddKeyToSetAsync(string key, string cacheKey)
    {
        await _database.SetAddAsync(key, cacheKey);
    }

    public async Task InvalidateAllKeysInSet(string setkey)
    {
        var keys = await _database.SetMembersAsync(setkey);
        foreach (var key in keys)
        {
            await _database.KeyDeleteAsync(new RedisKey(key));
        }
        await _database.KeyDeleteAsync(setkey);
    }
}
