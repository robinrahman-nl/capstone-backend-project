using Capstone.Data;
using Capstone.Interfaces;
using Capstone.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace Capstone;

class Program
{
    static void Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, config) =>
            {
                // Load configuration files for CLI app
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                // --- Infrastructure ---
                services.AddSingleton<IConfiguration>(context.Configuration);

                // Database depends on connection string from configuration
                services.AddSingleton<Database>(sp =>
                {
                    var config = sp.GetRequiredService<IConfiguration>();
                    var connectionString = config.GetConnectionString("DefaultConnection");

                    if (string.IsNullOrWhiteSpace(connectionString))
                        throw new InvalidOperationException(
                            "Missing connection string 'DefaultConnection'. Configure it via appsettings or environment variables.");

                    return new Database(connectionString);
                });

                // --- Repositories ---
                services.AddTransient<IProductRepository, ProductRepository>();
                services.AddTransient<ICustomerRepository, CustomerRepository>();
                services.AddTransient<IOrderRepository, OrderRepository>();

                // --- Services ---
                services.AddTransient<IAdminService, AdminService>();
                services.AddTransient<IProductService, ProductService>();
                services.AddTransient<ICustomerService, CustomerService>();

                // --- Entry point ---
                services.AddTransient<UI>();
            })
            .Build();

        // Resolve the entry point and run
        var ui = host.Services.GetRequiredService<UI>();
        ui.Run();
    }
}