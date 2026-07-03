public class OutboxOrderPayload
{
    public Guid OrderNumber {get; set;}
    public int CustomerId {get; set;}
    public decimal Amount {get; set;} 
}