using OrderService.Domain.Common;
using OrderService.Infrastructure.Messaging.Routing;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            await AddDomainEventsToOutboxMessage(cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }

        private async Task AddDomainEventsToOutboxMessage(CancellationToken cancellationToken)
        {
            //Get the entities extending BaseEntity and have DomainEvents.
            var domainEvents = _context.ChangeTracker.Entries<BaseEntity>().Select(e => e.Entity)
            .SelectMany(e => e.DomainEvents).ToList();

            //Create the Outbox message here
            foreach (var domainEvent in domainEvents)
            {
                var routingKey = RoutingKeyMapper.GetRoutingKey(domainEvent);
                var outbox = OutboxMessage.Create(domainEvent, routingKey);
                await _context.OutboxMessages.AddAsync(outbox, cancellationToken);
            }

            //clear the domain events
            foreach(var entity in _context.ChangeTracker.Entries<BaseEntity>())
            {
                entity.Entity.ClearDomainEvents();
            }
        }
    }
}
