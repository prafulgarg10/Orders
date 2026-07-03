using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class OutboxRepository : IOutboxRepository
    {
        private readonly AppDbContext _context;
        public OutboxRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Outbox>> GetAllAsync()
        {
            return await _context.Outboxes.ToListAsync();
        }

        public async Task<Outbox?> GetByIdAsync(Guid id)
        {
            return await _context.Outboxes.FindAsync(id);
        }

        public async Task AddAsync(Outbox outbox)
        {
            //don't save the changes here.
            await _context.Outboxes.AddAsync(outbox);
        }
    }
}
