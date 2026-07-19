public interface IProductService
{
    Task<IReadOnlyList<ProductDTO>> GetProductsAsync(IEnumerable<int> productIds, CancellationToken cancellationToken);
}