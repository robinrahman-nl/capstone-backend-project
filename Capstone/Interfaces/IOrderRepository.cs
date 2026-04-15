using Capstone.Models;
namespace Capstone.Interfaces;
public interface IOrderRepository
{
    List<Order> GetAllSubmittedOrders();
    Order? GetCartByCustomerId(int customerId);
    int CreateCart(int customerId);
    OrderDetails? GetOrderDetail(int orderId, int productId);
    int InsertOrderDetail(int orderId, int productId, int amount, double totalPrice);
    bool UpdateOrderDetail(int detailId, int newAmount, double newTotalPrice);
    bool DeleteOrderDetail(int detailId);
    List<OrderDetails> GetOrderDetailsByOrderId(int orderId);
    int UpdateOrderStatus(int orderId, string newStatus);
}  