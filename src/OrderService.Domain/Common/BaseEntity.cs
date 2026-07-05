using OrderService.Domain.Events;

namespace OrderService.Domain.Common;
public abstract class BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    //This extra variable is created to not let any other code to add or delete anything.
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}