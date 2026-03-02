namespace Capstone.Models;

public class OrderDetails
{
    public int DetailId { get; private set; }
    public int OrderId { get; private set; }
    public int ProductId { get; private set; }
    public int Amount { get; private set; }
    public double TotalPrice { get; private set; }

    public OrderDetails(int detailId, int orderId, int productId, int amount, double totalPrice)
    {
        DetailId = detailId;
        OrderId = orderId;
        ProductId = productId;
        Amount = amount;
        TotalPrice = totalPrice;
    }

    public override string ToString()
    {
        return
@"----------------
Order Line
----------------
Detail ID: " + DetailId + @"
Order ID: " + OrderId + @"
Product ID: " + ProductId + @"
Amount: " + Amount + @"
Total Price: " + TotalPrice + @"
";
    }
}