namespace Capstone.Models;

public class Order
{

    public int OrderId { get; private set; }
    public Customer Customer { get; private set; }
    public DateTime OrderDate  { get; private set; }  
    public string OrderStatus { get; private set; }
    public Order(int orderId, Customer customer, DateTime orderDate, string orderStatus)
    {
        OrderId = orderId;
        Customer = customer;
        OrderDate = orderDate;
        OrderStatus = orderStatus;
    }

    public override string ToString()
    {
        return @$"
        ----------------
        Order Details
        ----------------
        Customer:
        {Customer}
        
        ";
    }
}