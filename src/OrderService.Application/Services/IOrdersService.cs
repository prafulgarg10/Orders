public interface IOrderService
{
    Task<List<OrderResponse>> GetAllOrders();
    Task<OrderResponse?> GetOrderDetail(int id);
    Task<OrderResponse?> PlaceOrder(OrderRequest orderRequest);
}