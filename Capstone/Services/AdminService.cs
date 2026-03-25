using Capstone.Data;
using Capstone.Interfaces;

namespace Capstone.Services;

public class AdminService : IAdminService
{
    public readonly ProductRepository _productRepository;

    public AdminService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public bool AddProduct(string productName, string description, double productPrice, int quantityInStock)
    {
        int result = _productRepository.InsertProduct(productName, description, productPrice, quantityInStock);
        return result == 1;
    }

    public bool UpdateProduct(int id, string productName, string description, double productPrice, int quantityInStock)
    {
        int result = _productRepository.UpdateProduct(id, productName, description, productPrice, quantityInStock);
        return result > 0;
    }

    public bool DeleteProduct(int productId)
    {
        if (_productRepository.IsProductInUse(productId))
            return false;

        int result = _productRepository.DeleteProduct(productId);
        return result > 0;
    }
}