using Capstone.Models;
namespace Capstone.Interfaces;
public interface IProductRepository
{
    List<Product> GetAllProducts();
    int InsertProduct(string productName, string description, double productPrice, int quantityInStock);
    int UpdateProduct(int id, string productName, string description, double productPrice, int quantityInStock);
    int DeleteProduct(int id);
    bool IsProductInUse(int productId);
}