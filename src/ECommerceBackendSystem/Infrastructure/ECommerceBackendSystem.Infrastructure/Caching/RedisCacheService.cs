using ECommerceBackendSystem.Application.Abstractions.Caching;
using StackExchange.Redis;
using System.Text.Json;

namespace ECommerceBackendSystem.Infrastructure.Caching;

public class RedisCacheService(IDatabase database) : IRedisCacheService
{
    private readonly IDatabase _database = database;

    public bool KeyExists(string key)
       => this._database.KeyExists(key);

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await this._database.StringGetAsync(key);
        return value.HasValue ? JsonSerializer.Deserialize<T>(value!) : default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? ttl = null)
    {
        var json = JsonSerializer.Serialize(value);
        await this._database.StringSetAsync(key, json, ttl);
    }

    public async Task RemoveAsync(string key)
    {
        if (this._database.KeyExists(key))
            await this._database.KeyDeleteAsync(key);
    }
}
