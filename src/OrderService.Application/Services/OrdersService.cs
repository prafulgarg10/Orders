
public class OrdersService : IOrderService
{
    private IOrderRepository _repo;
    public OrdersService(IOrderRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<OrderResponse>> GetAllOrders()
    {
        var orders = await _repo.GetAllAsync();
        var result = orders.Select(o => new OrderResponse
        {
            OrderId = o.OrderId,
            CustomerName = "Praful",
            Amount = o.Amount,
            Status = o.Status,
            CreatedAt = o.CreatedAt
        }).ToList();
        return result;
    }

    public async Task<OrderResponse?> GetOrderDetail(int id)
    {
        var order = await _repo.GetByIdAsync(id);
        if (order != null)
        {
            var result = new OrderResponse
            {
                OrderId = order.OrderId,
                CustomerName = "Praful",
                Amount = order.Amount,
                Status = order.Status,
                CreatedAt = order.CreatedAt
            };
            return result;
        }
        return null;
    }

    public async Task<OrderResponse?> PlaceOrder(OrderRequest orderRequest)
    {
        if (orderRequest != null)
        {
            Order order = new Order(orderRequest.Amount, 1);
            await _repo.AddAsync(order);
            OrderResponse? response = await GetOrderDetail(order.OrderId);
            return response;
        }
        return null;
    }
}