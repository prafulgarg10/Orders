namespace OrderService.Application.Commands.CreateOrder;
//using record here as Commands are immutable
public sealed record CreateOrderCommand(int customerId, decimal Amount);
