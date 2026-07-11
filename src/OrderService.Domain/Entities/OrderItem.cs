public class OrderItem
{
    public int Id {get; private set;}
    public int OrderId {get; private set;}
    public int ProductId {get; private set;}
    public int Quantity {get; private set;}
    public decimal UnitPrice {get; private set;} 
    private OrderItem(){}

    public OrderItem(int productId, int quantity, decimal unitPrice)
    {
        if(productId<=0 || quantity<=0 || unitPrice <= 0)
        {
            throw new InvalidDataException("Failed to create order item");
        }
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }
}