using OrderService.Domain.Events;

public record OrderCreatedEvent(Guid OrderNumber, int CustomerId, decimal Amount) : IDomainEvent
{
    public DateTime OccuredOnUtc {get; } = DateTime.UtcNow; 
 
}