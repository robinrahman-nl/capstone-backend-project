using Capstone.Models;
namespace Capstone.Interfaces;

public interface IOrderDetailsRepository
{
    List<OrderDetails> GetAllOrderDetails();
}