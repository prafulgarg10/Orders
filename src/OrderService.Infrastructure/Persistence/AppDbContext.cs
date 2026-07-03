using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Order> Orders {get; set;}
    public DbSet<Outbox> Outboxes {get; set;}
}