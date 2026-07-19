public sealed record ProductsDTO(List<ProductDTO> products);

public sealed record ProductDTO(int ProductId, decimal UnitPrice);
