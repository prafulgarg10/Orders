namespace OrderService.Domain.Events;
public interface IDomainEvent
{
    DateTime OccuredOnUtc {get;}
}