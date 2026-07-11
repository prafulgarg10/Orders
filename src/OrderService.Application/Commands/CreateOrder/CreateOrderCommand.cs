namespace OrderService.Application.Commands.CreateOrder;
//using record here as Commands are immutable
public sealed record CreateOrderCommand(int customerId, List<Item> items);

public sealed record Item(int productId, int quantity, decimal unitPrice);
