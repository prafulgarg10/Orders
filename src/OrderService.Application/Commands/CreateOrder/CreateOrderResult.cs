namespace OrderService.Application.Commands.CreateOrder;
public sealed record CreateOrderResult(Guid OrderNumber, string Status);