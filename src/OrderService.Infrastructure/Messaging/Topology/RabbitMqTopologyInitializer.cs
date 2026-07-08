
using Microsoft.Extensions.Options;
using OrderService.Infrastructure.Messaging.Configurations;
using OrderService.Infrastructure.Messaging.Connections;
using RabbitMQ.Client;

namespace OrderService.Infrastructure.Messaging.Topology;

public class RabbitMqTopologyInitializer : IRabbitMqTopologyInitializer
{
    private readonly RabbitMqConnection _connection;
    private readonly RabbitMqOptions _options;
    public RabbitMqTopologyInitializer(RabbitMqConnection connection, IOptions<RabbitMqOptions> options)
    {
        _connection = connection;
        _options = options.Value;
    }
    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await using var channel = await _connection.CreateChannelAsync(cancellationToken);

        //ensure this exchange exists and re-used
        await channel.ExchangeDeclareAsync(
            exchange: _options.Exchange,
            type: _options.ExchangeType,
            durable: true,
            autoDelete: false,
            cancellationToken: cancellationToken);
    }
}