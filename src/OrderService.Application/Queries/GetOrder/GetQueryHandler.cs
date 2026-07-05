namespace OrderService.Application.Queries;
public sealed class GetQueryHandler
{
    private readonly IOrderRepository _orderRepository;
    public GetQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<GetQueryResult?> HandleAsync(GetOrderQuery query, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetByOrderNumberAsync(query.OrderNumber, cancellationToken);
    }
}