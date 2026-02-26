using System.Xml;
using Capstone.Interfaces;
using Capstone.Models;
using Capstone.Services;
using Capstone.Data;

namespace Capstone;

public class UI : IDisplayable
{
    public readonly AdminService _adminService;
    public readonly ProductService _productService;



    public UI(AdminService adminService, ProductService productService)
    {
        _adminService = adminService;
        _productService = productService;
    }

    /*
    ==========================================================================================
    Method: Display Main Menu. 
    ==========================================================================================
    */

    public void DisplayMainMenu()
    {
        Console.Clear();

        Console.WriteLine(@"
        =================================
          Welcome to pharmacyshop.com 
        =================================
        
        Please select an option
        [1] Show entire product catalogue. (Working)
        [2] Log in as a Customer. 
        [3] Log in as an Admin. (working)
        [4] Exit Program.
        ");
    }

    public void DisplayAdminMenu()
    {
        Console.Clear();

        Console.WriteLine(@"
        Please select an option
        [1] Add a product to the product catalogue. (working)
        [2] Edit a product in the product catalogue. (working)
        [3] Delete a product in the product catalogue.
        [4] Show all incoming orders. 
        [5] Go back to main menu. (working)
        ");
    }


    /*
    ==========================================================================================
    Method: Run Main Menu. 
    ==========================================================================================
    */

    public void Run()
    {
        bool isRunning = true;
        while (isRunning)
        {
            DisplayMainMenu();
            string userInputMainMenu = Console.ReadLine();

            switch (userInputMainMenu)
            {
                case "1":
                    ShowEntireProductCatalogue();
                    break;

                case "2":
                    Console.WriteLine("RunCustomerMenu();");
                    break;

                case "3":
                    AdminMenu();
                    break;


                case "4":
                    Console.WriteLine("Closing program. Goodbye");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }



    /*
    ==========================================================================================
    Method: Run Admin menu. 
    ==========================================================================================
    */

    public void AdminMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            DisplayAdminMenu();
            string userInputAdminnMenu = Console.ReadLine();

            switch (userInputAdminnMenu)
            {
                case "1":
                    AddProductFromInput();
                    break;

                case "2":
                    UpdateProductFromInput();
                    break;

                case "3":
                    DeleteProductFromInput();
                    break;

                case "4":
                    Console.WriteLine("ShowAllIncomingOrder()");
                    break;

                case "5":
                    Console.WriteLine("Returning to main menu.");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option please try again.");
                    break;
            }
        }
    }




    /*
    ==========================================================================================
    Method: Show the entire product catalogue. 
    ==========================================================================================
    */

    public void ShowEntireProductCatalogue()
    {
        var products = _productService.GetAllProducts();

        if (products.Count == 0)
        {
            Console.WriteLine("No products found.");
            return;
        }

        Console.WriteLine("\n--- Product Catalogue ---");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("--------------------------\n");
        Pause();
    }

    /*
    ==========================================================================================
    Method: Add product by Admin. [One product]
    ==========================================================================================
    */

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

        bool succes = _adminService.AddProduct(productNameInput, descriptionInput, productPrice, quantity);
        if (succes)
            Console.WriteLine("Product succesfully added.");
        else
            Console.WriteLine("Product adding failed.");
            Pause();
    }

    /*
   ==========================================================================================
   Method: Update product by Admin. 
   ==========================================================================================
   */
    public void UpdateProductFromInput()
    {
        int productIdInput;
        Console.WriteLine("Enter the Id of the product that you want to update (or type 'b' to go back):");

        while (true)
        {
            string input = Console.ReadLine();

            if (input?.ToLower() == "b")
            {
                Console.WriteLine("Operation cancelled. Returning to admin menu...");
                return;
            }

            if (int.TryParse(input, out productIdInput))
                break;

            Console.WriteLine("Invalid input. Please enter a valid number or 'b' to go back:");
        }

        // Ask for product name to update.
        Console.WriteLine("Enter product name.");
        string productNameInput = Console.ReadLine();

        // Ask for product description to update. 
        Console.WriteLine("Enter product description to update.");
        string descriptionInput = Console.ReadLine();

        // Ask for Product price to update. 
        double productPriceInput;
        Console.WriteLine("Enter product price to update.");
        while (!double.TryParse(Console.ReadLine(), out productPriceInput))
        {
            Console.WriteLine("Invalid price. Please enter a valid number.");
        }

        // Ask for product quantity to update.
        int productQuantity;
        Console.WriteLine("Enter product quantity to update.");
        while (!int.TryParse(Console.ReadLine(), out productQuantity))
        {
            Console.WriteLine("Invalid price. Please enter a valid number.");
        }

        bool succes = _adminService.UpdateProduct(productIdInput, productNameInput, descriptionInput, productPriceInput, productQuantity);
        if (succes)
            Console.WriteLine("Product succesfully updated.");
        else
            Console.WriteLine("Product update failed.");
            Pause();


    }

    /*
    ==========================================================================================
    Method: Delete a product by Admin. [One product]
    ==========================================================================================
    */

    public void DeleteProductFromInput()
    {
        int productIdInput;
        Console.WriteLine("Enter the Id of the product that you want to delete (or type 'b' to go back):");

        while (true)
        {
            string input = Console.ReadLine();

            if (input?.ToLower() == "b")
            {
                Console.WriteLine("Operation cancelled. Returning to admin menu...");
                return;
            }

            if (int.TryParse(input, out productIdInput))
                break;

            Console.WriteLine("Invalid input. Please enter a valid number or 'b' to go back:");
        }

        bool succes = _adminService.DeleteProduct(productIdInput);

        if (succes)
            Console.WriteLine("Product successfully deleted.");
        else
            Console.WriteLine("Product deletion failed.");
        Pause();
    }

    /*
    ==========================================================================================
    Helper Method: Pause screen
    ==========================================================================================
    */
    private void Pause()
    {
        Console.WriteLine("\nPress any key to go back to menu.");
        Console.ReadKey();
    }
}
