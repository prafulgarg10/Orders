public interface IOutboxRepository
{
    Task<List<Outbox>> GetAllAsync();
    Task<Outbox?> GetByIdAsync(Guid id);
    Task AddAsync(Outbox outbox);
}