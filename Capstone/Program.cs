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
        CustomerRepository customerRepository = new CustomerRepository(database1);
        OrderRepository orderRepository = new OrderRepository(database1);
        CustomerService customerService = new CustomerService(productRepository, customerRepository, orderRepository);  

        UI ui = new UI(adminService, productService, customerService);

        ui.Run();
    }
}