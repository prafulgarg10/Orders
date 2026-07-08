using RabbitMQ.Client;

public interface IPublisherConfirmationAwaiter
{
    Task ExecuteConfirmationAsync(Func<Task> publishOperation, IChannel channel, CancellationToken cancellationToken);
}