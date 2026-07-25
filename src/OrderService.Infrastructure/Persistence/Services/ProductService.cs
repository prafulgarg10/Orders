using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using OrderService.Application.Services;
using OrderService.Infrastructure.Caching;

namespace OrderService.Infrastructure.Persistence.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private readonly ICacheService _cache;
    private readonly ILogger<ProductService> _logger;

    public ProductService(HttpClient httpClient, ICacheService cache, ILogger<ProductService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ProductDTO>> GetProductsAsync(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        List<string> keys = productIds.Select(i => CacheKeys.Product(i)).ToList();
        Dictionary<string, ProductDTO?>? cachedProducts = await _cache.GetMultipleAsync<ProductDTO>(keys);
        //cache hit
        if (cachedProducts != null && !cachedProducts.Values.Any(v => v==default)) 
        {
            _logger.LogInformation("Products {ProductIds} were served from cache.", productIds);
            return cachedProducts.Values.ToList()!;
        }
        //cache miss
        List<int> missingProductIds = productIds.ToList();
        if (cachedProducts != null)
        {
            List<string> missingProductsKeys = cachedProducts.Where(kvp => kvp.Value==default).Select(kvp => kvp.Key).ToList();
            missingProductIds = missingProductIds.Where(p => missingProductsKeys.Contains(CacheKeys.Product(p))).ToList();
            _logger.LogWarning("Products {MissingProductIds} not found in cache.", missingProductIds);
        }
        var response = await _httpClient.PostAsJsonAsync($"api/products", new { productIds = missingProductIds }, cancellationToken);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<ProductsDTO>(cancellationToken: cancellationToken);
        List<ProductDTO>? products = result?.products;
        if (products != null)
        {
            await _cache.SetMultipleAsync(products.Select(p => new KeyValuePair<string, ProductDTO>(CacheKeys.Product(p.ProductId), p)).ToList(), TimeSpan.FromMinutes(10));
            //add already cached products
            if (cachedProducts != null && cachedProducts.Values.Any(v => v!=default))
            {
                products.AddRange(cachedProducts.Where(kvp => kvp.Value!=null).Select(kvp => kvp.Value).AsEnumerable()!);
            }
        }
        return products ?? new List<ProductDTO>();        
    }
}