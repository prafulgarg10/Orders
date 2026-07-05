public interface IOutboxMessageRepository
{
    Task<List<OutboxMessage>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<OutboxMessage?> GetByMessageIdAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task AddAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken = default);
}