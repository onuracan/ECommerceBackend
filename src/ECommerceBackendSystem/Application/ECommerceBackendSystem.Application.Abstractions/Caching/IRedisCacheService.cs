namespace ECommerceBackendSystem.Application.Abstractions.Caching;

public interface IRedisCacheService
{
    bool KeyExists(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? ttl = null);
    Task<T> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}
