namespace OrderService.Infrastructure.Messaging.Topology;
public interface IRabbitMqTopologyInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
}