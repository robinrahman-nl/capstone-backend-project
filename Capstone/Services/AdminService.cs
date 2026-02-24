using Capstone.Data;

namespace Capstone.Services;

public class AdminService
{
    public readonly ProductRepository _productRepository;

    public AdminService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void AddProduct(string productName, string description, double productPrice, int quantityInStock)
    {   
        _productRepository.InsertProduct(productName, description, productPrice, quantityInStock);
    }
}