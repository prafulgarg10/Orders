using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Orders.ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Orders.FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        //don't save the changes here.
        await _context.Orders.AddAsync(order, cancellationToken);
    }
}
}
