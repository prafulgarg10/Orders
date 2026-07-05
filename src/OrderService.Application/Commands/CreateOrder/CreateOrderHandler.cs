namespace OrderService.Application.Commands.CreateOrder;

public sealed class CreateOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOutboxMessageRepository _outboxMessageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(IOrderRepository orderRepository, IOutboxMessageRepository outboxMessageRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _outboxMessageRepository = outboxMessageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateOrderResult> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = new Order(command.Amount, command.customerId);
        // var orderCreatedEvent = new OrderCreatedEvent(order.OrderNumber, order.CustomerId, order.Amount);
        // var outbox = OutboxMessage.Create(orderCreatedEvent);
        await _orderRepository.AddAsync(order, cancellationToken);
        //await _outboxMessageRepository.AddAsync(outbox, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return new CreateOrderResult(order.OrderNumber, order.Status.ToString());
    }
}