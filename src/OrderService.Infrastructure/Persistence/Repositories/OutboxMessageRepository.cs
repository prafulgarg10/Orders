using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly AppDbContext _context;
        public OutboxMessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OutboxMessage>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.OutboxMessages.ToListAsync(cancellationToken);
        }

        public async Task<OutboxMessage?> GetByMessageIdAsync(Guid messageId, CancellationToken cancellationToken)
        {
            return await _context.OutboxMessages.FindAsync(messageId, cancellationToken);
        }

        public async Task AddAsync(OutboxMessage outboxMessage, CancellationToken cancellationToken)
        {
            //don't save the changes here.
            await _context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        }
    }
}
