namespace OrderService.Infrastructure.Caching.Configurations;

public sealed class RedisOptions
{
    public const string SectionName = "Redis";
    public string ConnectionString {get; init;} = string.Empty;
    public bool AbortOnConnectionFail {get; init;} = false;
    public int ConnectRetry {get; init;} = 1;
}