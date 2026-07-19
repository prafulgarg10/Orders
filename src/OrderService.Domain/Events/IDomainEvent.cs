namespace OrderService.Domain.Events;
public interface IDomainEvent
{
    DateTime OccuredOnUtc {get;}
    public Guid MessageId {get; }
}