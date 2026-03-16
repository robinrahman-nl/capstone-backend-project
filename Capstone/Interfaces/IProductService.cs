using Capstone.Models;

namespace Capstone.Interfaces;

public interface IProductService
{
    List<Product> GetAllProducts();
}