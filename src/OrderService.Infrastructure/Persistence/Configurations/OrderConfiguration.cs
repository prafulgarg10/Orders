using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);
        builder.Property(o => o.OrderNumber).IsRequired();
        builder.Property(o => o.Amount).HasPrecision(18,2);
        //one to many relation.
        builder.HasMany(o => o.OrderItems).WithOne().HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(o => o.OrderItems).HasField("_items").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}