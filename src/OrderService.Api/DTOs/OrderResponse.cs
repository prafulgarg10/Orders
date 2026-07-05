public class OrderResponse
{
    public int OrderId {get; set;}
    public string CustomerName {get; set;}
    public decimal Amount {get; set;}
    public Status Status {get; set;}
    public DateTime CreatedAt {get; set;}
}