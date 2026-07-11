using System.Collections.Immutable;

namespace OrderService.Application.Queries.GetOrders;
public sealed class GetOrdersHandler
{
    private readonly IOrderRepository _orderRepository;
    public GetOrdersHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetOrdersResult> HandleAsync(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var response = await _orderRepository.GetAllAsync(query.PageNumber, query.PageSize, query.CustomerId, cancellationToken);
        return new GetOrdersResult((IReadOnlyList<OrderSummary>)response.Select(o => new OrderSummary(o.OrderNumber, o.Amount, o.Status.ToString(), o.CreatedAt, o.OrderItems.Select(o => new OrderItemSummary(o.ProductId, o.Quantity, o.UnitPrice)).ToList())).ToList());
    }
}