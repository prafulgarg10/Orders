using System.ComponentModel.DataAnnotations;
using OrderService.Domain.Common;

public class Order : BaseEntity
{
    public int OrderId {get; private set;}
    public Guid OrderNumber {get; private set;}
    public int CustomerId {get; private set;}
    public decimal Amount {get; private set;}
    public Status Status {get; private set;}
    public DateTime CreatedAt {get; private set;}
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> OrderItems => _items.AsReadOnly();
    public void AddItem(int productId, int quantity, decimal unitPrice)
    {
        _items.Add(new OrderItem(productId, quantity, unitPrice));
    }
    private Order(){}
    public Order(int customerId)
    {
        CustomerId = customerId;
        Status = Status.Created;
        CreatedAt = DateTime.UtcNow;
        OrderNumber = Guid.NewGuid();
    }

    private void MarkAsCreated()
    {
        //For letting the unit of work to create the outbox entry
        RaiseDomainEvent(new OrderCreatedDomainEvent(OrderNumber, CustomerId, Amount, OrderItems.Select(i => new Items(i.ProductId, i.Quantity)).ToList()));
    }

    public void Place()
    {
        if (Status != Status.Created)
        {
            throw new InvalidOperationException("Order already placed");
        }
        if (OrderItems.Count == 0)
        {
            throw new InvalidOperationException("Order must contain atleast one item");
        }
        Amount = OrderItems.Sum(o => o.UnitPrice*o.Quantity);
        MarkAsCreated();
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