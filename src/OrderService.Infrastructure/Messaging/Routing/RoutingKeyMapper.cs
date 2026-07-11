using OrderService.Domain.Events;

namespace OrderService.Infrastructure.Messaging.Routing;
public static class RoutingKeyMapper
{
    public static string GetRoutingKey(IDomainEvent domainEvent)
    {
        return domainEvent switch
        {
            OrderCreatedDomainEvent => "order.created",
            _ => throw new NotSupportedException($"No routing key confgured for {domainEvent.GetType().Name}")
        };
    }
}