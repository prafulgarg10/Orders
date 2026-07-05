using System.ComponentModel.DataAnnotations;
using OrderService.Domain.Common;

public class Order : BaseEntity
{
    [Key]
    public int OrderId {get; private set;}
    public Guid OrderNumber {get; private set;}
    public int CustomerId {get; private set;}
    public decimal Amount {get; private set;}
    public Status Status {get; private set;}
    public DateTime CreatedAt {get; private set;}

    public Order(decimal amount, int customerId)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Please provide correct amount");
        }
        Amount = amount;
        CustomerId = customerId;
        Status = Status.Created;
        CreatedAt = DateTime.UtcNow;
        OrderNumber = Guid.NewGuid();
    }

    public static Order Create(decimal amount, int customerId)
    {
        var order = new Order(amount, customerId);
        order.MarkAsCreated();
        return order;
    }

    private void MarkAsCreated()
    {
        //For letting the unit of work to create the outbox entry
        RaiseDomainEvent(new OrderCreatedEvent(OrderNumber, CustomerId, Amount));
    }

    public void Complete()
    {
        if (this.Status == Status.Created)
        {
            this.Status = Status.Completed;
        }
        else
        {
            throw new InvalidOperationException("Invalid order status");
        }
    }

    public void Cancel()
    {
        if (!(this.Status==Status.Completed))
        {
            this.Status = Status.Cancelled;
        }
        else
        {
            throw new InvalidOperationException("Order cannot be cancelled after completion");
        }
    }

}