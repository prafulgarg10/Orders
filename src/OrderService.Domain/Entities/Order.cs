using System.ComponentModel.DataAnnotations;

public class Order
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
        OrderNumber = new Guid();
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