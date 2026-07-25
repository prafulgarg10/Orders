namespace OrderService.Infrastructure.Caching;

public static class CacheKeys
{
    public static string Product(int id) => $"product:{id}";
}