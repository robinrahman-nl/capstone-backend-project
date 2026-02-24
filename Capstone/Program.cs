using Capstone.Data;
using Capstone.Models;
using Capstone.Services;

namespace Capstone;

class Program
{
    static void Main(string[] args)
    {

        Database database1 = new Database();
        ProductRepository productRepository3 = new ProductRepository(database1);
        AdminService adminService3 = new AdminService(productRepository3);
        UI uI3 = new UI(adminService3);

        uI3.AddProductFromInput();
    }
}