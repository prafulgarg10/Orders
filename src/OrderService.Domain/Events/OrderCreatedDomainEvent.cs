using OrderService.Domain.Events;

public record OrderCreatedDomainEvent(Guid OrderNumber, int CustomerId, decimal Amount, List<Items> items) : IDomainEvent
{
    public DateTime OccuredOnUtc {get; } = DateTime.UtcNow; 
    public Guid MessageId {get; } = Guid.NewGuid();
}

public record Items(int ProductId, int Quantity, decimal UnitPrice);