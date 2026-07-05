
using System.Text.Json;

public class OrdersService : IOrderService
{
    private IOrderRepository _repo;
    private IOutboxMessageRepository _outboxRepo;
    private IUnitOfWork _unitOfWorkRepo;
    public OrdersService(IOrderRepository repo, IOutboxMessageRepository outboxRepository, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _outboxRepo = outboxRepository;
        _unitOfWorkRepo = unitOfWork;
    }

    public async Task<List<OrderResponse>> GetAllOrders(CancellationToken cancellationToken)
    {
        var orders = await _repo.GetAllAsync(cancellationToken);
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

    public async Task<OrderResponse?> GetOrderDetail(int id, CancellationToken cancellationToken)
    {
        var order = await _repo.GetByIdAsync(id, cancellationToken);
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

    public async Task<OrderResponse?> PlaceOrder(OrderRequest orderRequest, CancellationToken cancellationToken)
    {
        if (orderRequest != null)
        {
            Order order = new Order(orderRequest.Amount, 1);
            await _repo.AddAsync(order, cancellationToken);

            //to save message to outboxmessage table so that it can be consumed by the consumers.
            var evt = new OrderCreatedEvent(order.OrderNumber, order.CustomerId, order.Amount);
            OutboxMessage outbox = OutboxMessage.Create(evt);
            await _outboxRepo.AddAsync(outbox, cancellationToken);

            //save all the changes in one transaction. If failed then all changes will be reverted.
            await _unitOfWorkRepo.SaveChangesAsync(cancellationToken);

            OrderResponse? response = await GetOrderDetail(order.OrderId, cancellationToken);
            return response;
        }
        return null;
    }
}