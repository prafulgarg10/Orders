public interface IOrderService
{
    Task<List<OrderResponse>> GetAllOrders(CancellationToken cancellationToken);
    Task<OrderResponse?> GetOrderDetail(int id, CancellationToken cancellationToken);
    Task<OrderResponse?> PlaceOrder(OrderRequest orderRequest, CancellationToken cancellationToken);
}