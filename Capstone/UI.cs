using Capstone.Services;

namespace Capstone;

public class UI
{
    public readonly AdminService _adminService;

    public UI(AdminService adminService)
    {
        _adminService = adminService;
    }

    public void AddProductFromInput()
    {

        Console.WriteLine("Enter product name.");
        string productNameInput = Console.ReadLine();

        Console.WriteLine("Enter product description");
        string descriptionInput = Console.ReadLine();

        // Ask for price.
        double productPrice;
        Console.WriteLine("Enter product price:");
        while (!double.TryParse(Console.ReadLine(), out productPrice))
        {
            Console.WriteLine("Invalid price. Please enter a valid number.");
        }

        // Ask for quantity of product.
        int quantity;
        Console.WriteLine("Enter product quantity:");
        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.WriteLine("Invalid input. Please enter a valid number:");
        }

        _adminService.AddProduct(productNameInput, descriptionInput, productPrice, quantity);
    }
}
