namespace Capstone.Interfaces;

public interface IAdminService
{
    bool AddProduct(string productName, string description, double productPrice, int quantityInStock);
    bool UpdateProduct(int id, string productName, string description, double productPrice, int quantityInStock);
    bool DeleteProduct(int id);
}