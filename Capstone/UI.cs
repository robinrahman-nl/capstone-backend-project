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
        Console.WriteLine(@"
        =================================
          Welcome to pharmacyshop.com 
        =================================
        
        Please select an option
        [1] Show entire product catalogue.
        [2] Log in as a Customer.
        [3] Log in as an Admin.
        [4] Exit Program.
        ");
    }

    public void DisplayAdminMenu()
    {
        Console.WriteLine(@"
        Please select an option
        [1] Add a product to the product catalogue.
        [2] Edit a product in the product catalogue.
        [3] delete a product in the product catalogue.
        [4] Show all incoming orders. (complete or reject). 
        [5] Go back to main menu. 
        [6] Exit Program. 
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
                    Console.WriteLine("EditProductFromInput()");
                    break;

                case "3":
                    Console.WriteLine("DeleteProductFromInput()");
                    break;

                case "4":
                    Console.WriteLine("ShowAllIncomingOrder()");
                    break;
                
                case "5":
                    Run();
                    break;

                case "6":
                    Console.WriteLine("Closing program goodbye.");
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
    Method: Add product by Admin. 
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
    }

}
