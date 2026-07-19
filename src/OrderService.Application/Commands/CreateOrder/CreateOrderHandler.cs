namespace OrderService.Application.Commands.CreateOrder;

public sealed class CreateOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOrderHandler(IOrderRepository orderRepository, IProductService productService, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productService = productService;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateOrderResult> HandleAsync(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        //get the product details from ProductService
        var productIds = command.items.Select(i => i.productId).Distinct().ToList();
        var products = await _productService.GetProductsAsync(productIds, cancellationToken);
        Dictionary<int, ProductDTO> productDictionary = products.ToDictionary(p => p.ProductId);

        //create the order and add items to it
        var order = new Order(command.customerId);
        foreach(Item item in command.items)
        {
            if (!productDictionary.TryGetValue(item.productId, out var product))
            {
                throw new InvalidOperationException(
                    $"Product {item.productId} not found.");
            }
            order.AddItem(item.productId, item.quantity, product.UnitPrice);
        }
        order.Place();
        // var orderCreatedEvent = new OrderCreatedEvent(order.OrderNumber, order.CustomerId, order.Amount);
        // var outbox = OutboxMessage.Create(orderCreatedEvent);
        await _orderRepository.AddAsync(order, cancellationToken);
        //await _outboxMessageRepository.AddAsync(outbox, cancellationToken);
        await _unitOfWork.SaveChangesAsync();
        return new CreateOrderResult(order.OrderNumber, order.Status.ToString());
    }
}