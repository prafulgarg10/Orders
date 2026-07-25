namespace OrderService.Application.Services;
public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
    Task<Dictionary<string, T?>?> GetMultipleAsync<T>(List<string> key, CancellationToken cancellationToken = default);
    Task SetAsync<T>(string key, T value, TimeSpan expiry, CancellationToken cancellationToken = default);
    Task SetMultipleAsync<T>(List<KeyValuePair<string, T>> keyValuePairs, TimeSpan expiry, CancellationToken cancellationToken = default);
    Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default);
}