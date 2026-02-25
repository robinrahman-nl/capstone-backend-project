using Capstone.Data;
using Capstone.Models;
namespace Capstone.Services;

public class ProductService
{
    public readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }
}



