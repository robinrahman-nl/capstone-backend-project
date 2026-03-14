using Capstone.Data;
using Capstone.Models;
using Capstone.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Capstone;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
        .Build();

        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Missing connection string 'DefaultConnection'. Configure it via appsettings or environment variables.");

        Database database = new Database(connectionString);

        ProductRepository productRepository = new ProductRepository(database);
        AdminService adminService = new AdminService(productRepository);
        ProductService productService = new ProductService(productRepository);
        CustomerRepository customerRepository = new CustomerRepository(database);
        OrderRepository orderRepository = new OrderRepository(database);
        CustomerService customerService = new CustomerService(productRepository, customerRepository, orderRepository);

        UI ui = new UI(adminService, productService, customerService);

        ui.Run();
    }
}