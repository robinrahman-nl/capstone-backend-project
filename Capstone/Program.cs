using Capstone.Data;
using Capstone.Models;
using Capstone.Services;

namespace Capstone;

class Program
{
    static void Main(string[] args)
    {
        Database database1 = new Database();

        ProductRepository productRepository = new ProductRepository(database1);
        AdminService adminService = new AdminService(productRepository);
        ProductService productService = new ProductService(productRepository);

        UI ui = new UI(adminService, productService);

        ui.Run();
    }
}