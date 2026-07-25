using System.Text.Json;
using Microsoft.Extensions.Logging;
using OrderService.Application.Services;
using StackExchange.Redis;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly ILogger<RedisCacheService> _logger;
    public RedisCacheService(IConnectionMultiplexer connection, ILogger<RedisCacheService> logger)
    {
        _database = connection.GetDatabase();
        _logger = logger;
    }
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            RedisValue val = await _database.StringGetAsync(key);
            if (val.IsNullOrEmpty)
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(val!);
        }
        catch(RedisException ex)
        {
            _logger.LogError($"Redis connection failure. Switch to default database data. {ex.Message}");
            return default;
        }

    }

    public async Task<Dictionary<string, T?>?> GetMultipleAsync<T>(List<string> keys, CancellationToken cancellationToken = default)
    {
        try
        {
            RedisKey[] rks = keys.Select(k => (RedisKey)k).ToArray();
            RedisValue[] values = await _database.StringGetAsync(rks);
            if (values.Count() == 0)
            {
                return default;
            }
            Dictionary<string, T?> keyValuePairs = new Dictionary<string, T?>();
            for (int i = 0; i < rks.Length; i++)
            {
                //to keep track of missing keys
                if (values[i].IsNullOrEmpty)
                {
                    keyValuePairs[rks[i]!] = default;
                }
                else
                {
                    keyValuePairs[rks[i]!] = JsonSerializer.Deserialize<T>(values[i]!);
                }
            }
            return keyValuePairs;
        }
        catch(RedisException ex)
        {
            _logger.LogError($"Redis connection failure. Switch to default database data. {ex.Message}");
            return default;
        }
    }

    public async Task<bool> RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return await _database.KeyDeleteAsync(key);
    }

    public async Task SetAsync<T>(string key, T val, TimeSpan expiry, CancellationToken cancellationToken = default)
    {
        await _database.StringSetAsync(key, JsonSerializer.Serialize(val), expiry);
    }

    public async Task SetMultipleAsync<T>(List<KeyValuePair<string, T>> keyValuePairs, TimeSpan expiry, CancellationToken cancellationToken = default)
    {
        try
        {
            KeyValuePair<RedisKey, RedisValue>[] values = keyValuePairs.Select(k => new KeyValuePair<RedisKey, RedisValue>(k.Key, JsonSerializer.Serialize(k.Value))).ToArray();
            var batch = _database.CreateBatch();
            var tasks = new Task[values.Length];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = batch.StringSetAsync(values[i].Key, values[i].Value, expiry);
            }
            batch.Execute();
            await Task.WhenAll(tasks);
        }
        catch(RedisException ex)
        {
            _logger.LogError($"Redis connection failure. Switch to default database data. {ex.Message}");
        }

    }
}