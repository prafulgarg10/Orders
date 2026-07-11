namespace OrderService.Application.Queries.GetOrders;
public sealed record GetOrdersResult(IReadOnlyList<OrderSummary> Orders);

public sealed record OrderSummary(Guid OrderNumber, decimal Amount, string Status, DateTime CreatedAt, List<OrderItemSummary> OrderItemSummaries);

public sealed record OrderItemSummary(int ProductId, int Quantity, decimal UnitPrice);