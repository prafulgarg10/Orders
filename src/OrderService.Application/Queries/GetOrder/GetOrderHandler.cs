namespace OrderService.Application.Queries.GetOrder;
public sealed class GetOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    public GetOrderHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrderResult?> HandleAsync(GetOrderQuery query, CancellationToken cancellationToken)
    {
        var response = await _orderRepository.GetByOrderNumberAsync(query.OrderNumber, cancellationToken);
        if (response != null)
        {
            return new GetOrderResult(response.OrderNumber, response.Amount, response.Status.ToString(), response.CreatedAt, response.OrderItems.Select(i => new GetOrderItem(i.ProductId, i.Quantity, i.UnitPrice)).ToList());
        }
        return null;
    }
}