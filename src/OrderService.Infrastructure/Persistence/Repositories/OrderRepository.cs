using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Queries;

namespace OrderService.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync(int pageNumber, int pageSize, int customerId, CancellationToken cancellationToken)
    {
        return await _context.Orders.Where(o => o.CustomerId==customerId).OrderByDescending(o => o.CreatedAt).Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Orders.FindAsync(id, cancellationToken);
    }

    public async Task<Order?> GetByOrderNumberAsync(Guid orderNumber, CancellationToken cancellationToken)
    {
        var result = await _context.Orders.Where(o => o.OrderNumber==orderNumber).FirstOrDefaultAsync(cancellationToken);
        return result;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        //don't save the changes here.
        await _context.Orders.AddAsync(order, cancellationToken);
    }
}
}
