namespace Capstone.Models;

public class OrderDetails
{
    public int DetailId { get; private set; }
    public Order Order { get; private set; }
    public Product Product { get; private set; }
    public int Amount { get; private set; }
    public double TotalPrice { get; private set; }

    public OrderDetails(int detailId, Order order, Product product, int amount, double totalPrice)
    {
        DetailId = detailId;
        Order = order;
        Product = product;
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
Order ID: " + Order.OrderId + @"
Product: " + Product.ProductName + @"
Amount: " + Amount + @"
Total Price: " + TotalPrice + @"
";
    }
}