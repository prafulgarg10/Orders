namespace OrderService.Application.Queries.GetOrder;
public sealed record GetOrderResult(Guid OrderNumber, decimal Amount, string Status, DateTime CreatedAt);