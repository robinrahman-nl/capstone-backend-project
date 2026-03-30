using Capstone.Models;

namespace Capstone.Interfaces;
public interface ICustomerService
{
    int GetCustomerIdByUserName(string username);
    Order GetOrCreateCart(int customerId);
    bool AddProductToCart(int customerId, int productId, int quantity);
    bool RemoveProductFromCart(int customerId, int productId, int quantityToRemove);
}