using System.Net.Http.Json;

namespace OrderService.Infrastructure.Persistence.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<ProductDTO>> GetProductsAsync(IEnumerable<int> productIds, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/products", productIds, cancellationToken);
        response.EnsureSuccessStatusCode();
        var products = await response.Content.ReadFromJsonAsync<List<ProductDTO>>(cancellationToken: cancellationToken);
        return products ?? new List<ProductDTO>();
    }
}