namespace OrderService.Application.Queries.GetOrder;
public sealed record GetOrderResult(Guid OrderNumber, decimal Amount, string Status, DateTime CreatedAt, List<GetOrderItem> GetOrderItems);
public sealed record GetOrderItem(int ProductId, int Quantity, decimal UnitPrice);