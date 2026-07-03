
using System.Text.Json;

public class OrdersService : IOrderService
{
    private IOrderRepository _repo;
    private IOutboxRepository _outboxRepo;
    private IUnitOfWork _unitOfWorkRepo;
    public OrdersService(IOrderRepository repo, IOutboxRepository outboxRepository, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _outboxRepo = outboxRepository;
        _unitOfWorkRepo = unitOfWork;
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

            OutboxOrderPayload payload = new OutboxOrderPayload()
            {
                OrderNumber = order.OrderNumber,
                CustomerId = order.CustomerId,
                Amount = order.Amount
            };
            Outbox outbox = new Outbox(Event.OrderCreated, JsonSerializer.Serialize(payload));
            await _outboxRepo.AddAsync(outbox);

            await _unitOfWorkRepo.SaveChangesAsync();

            OrderResponse? response = await GetOrderDetail(order.OrderId);
            return response;
        }
        return null;
    }
}