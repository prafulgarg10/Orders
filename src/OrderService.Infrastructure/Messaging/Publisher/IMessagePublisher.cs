namespace OrderService.Infrastructure.Messaging.Publisher;
public interface IMessagePublisher
{
    Task PublishAsync(string routingKey, object message, CancellationToken cancellationToken = default);
}