using OrderService.Application.Queries;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync(int pageNumber, int pageSize, int customerId, CancellationToken cancellationToken = default);
    Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Order?> GetByOrderNumberAsync(Guid orderNumber, CancellationToken cancellationToken = default);
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
}