using Capstone.Data;
using Capstone.Models;
using Capstone.Interfaces;
namespace Capstone.Services;

public class ProductService: IProductService
{
    public readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public List<Product> GetAllProducts()
    {
        return _productRepository.GetAllProducts();
    }
}