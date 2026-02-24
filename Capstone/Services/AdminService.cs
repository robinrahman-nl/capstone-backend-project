using Capstone.Data;

namespace Capstone.Services;

public class AdminService
{
    public readonly ProductRepository _productRepository;

    public AdminService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool AddProduct(string productName, string description, double productPrice, int quantityInStock)
    {   
        int result =_productRepository.InsertProduct(productName, description, productPrice, quantityInStock);
        return result ==1;
    }
}