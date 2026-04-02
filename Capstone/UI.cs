using System.Xml;
using Capstone.Interfaces;
using Capstone.Models;
using Capstone.Services;
using Capstone.Data;
using System.Linq;

namespace Capstone;

public class UI : IDisplayable
{
    public readonly IAdminService _adminService;
    public readonly IProductService _productService;
    private string _currentCustomerUserName;
    private int _currentCustomerId;
    public readonly ICustomerService _customerService;




    public UI(IAdminService adminService, IProductService productService, ICustomerService customerService)
    {
        _adminService = adminService;
        _productService = productService;
        _customerService = customerService;
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

        [1] Show entire product catalogue. {Working}
        [2] Log in as a Customer. {working}
        [3] Log in as an Admin. {working}
        [4] Exit Program. {working}
        ");
    }

    /*
    ==========================================================================================
    Method: Display Admin Menu. 
    ==========================================================================================
    */

    public void DisplayAdminMenu()
    {
        Console.Clear();

        Console.WriteLine(@"
        Please select an option

        [0] Show entire product catalogue. {working}
        [1] Add a product to the product catalogue. {working}
        [2] Edit a product in the product catalogue. {working}
        [3] Delete a product in the product catalogue. {working}
        [4] Show all incoming orders. 
        [5] Go back to main menu. {working}
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
                    RunCustomerMenu();
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

                case "0":
                    ShowEntireProductCatalogue();
                    break;

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

        // Fetch product to show details before deletion
        var products = _productService.GetAllProducts();
        var product = products.FirstOrDefault(p => p.ProductId == productIdInput);

        if (product == null)
        {
            Console.WriteLine("Product not found.");
            Pause();
            return;
        }

        // User confirmation before deletion. 
        Console.WriteLine("\nYou are about to delete the following product:");
        Console.WriteLine(product);
        Console.WriteLine("Are you sure you want to delete this product? (y/n)");

        string confirmation = Console.ReadLine()?.ToLower();

        if (confirmation != "y")
        {
            Console.WriteLine("Deletion cancelled.");
            Pause();
            return;
        }

        // Delete product. 
        bool succes = _adminService.DeleteProduct(productIdInput);

        if (succes)
        {
            Console.WriteLine("Product successfully deleted.");
        }
        else
        {
            Console.WriteLine("Cannot delete this product because this product is in a placed order.");
            Console.WriteLine("Caution: To delete this product, reject all orders containing this product first.");
        }

        Pause();
    }

    /*
    ==========================================================================================
    Method: Display Customer Menu. 
    ==========================================================================================
    */
    public void DisplayCustomerMenu()
    {
        Console.Clear();

        Console.WriteLine(@"
        Please select an option
        [0] Show entire product catalogue. {working}
        [1] View details of a specific product (by ID). {working}
        [2] Add product to cart. {working}
        [3] View cart.
        [4] Remove product from cart (decrease quantity).
        [5] Place order.
        [6] Go back to main menu. 
        ");
    }

    /*
    ==========================================================================================
    Method: Customer Menu.
    ==========================================================================================
    */

    public void RunCustomerMenu()
    {
        // ----------------------------------------------------------------------------------------
        // Set current customer id
        // Ask for customer username and store it internally.
        Console.WriteLine("Please Enter your username");
        _currentCustomerUserName = Console.ReadLine();

        // Get current customer Id.
        _currentCustomerId = _customerService.GetCustomerIdByUserName(_currentCustomerUserName);

        if (_currentCustomerId == -1)
        {
            Console.WriteLine("User name not found. Returning to main menu.");
            Pause();
            return; // exit customer menu. 
        }

        // ----------------------------------------------------------------------------------------



        bool isRunning = true;
        while (isRunning)
        {
            DisplayCustomerMenu();
            string userInputCustomerMenu = Console.ReadLine();

            switch (userInputCustomerMenu)
            {
                case "0":
                    ShowEntireProductCatalogue();
                    break;

                case "1":
                    ShowProductById();
                    break;

                case "2":
                    AddProductToCartFromInput();
                    break;

                case "3":
                    ViewCartforCurrentCustomer();
                    break;

                case "4":
                    RemoveProductFromCartFromInput();
                    break;

                case "5":
                    Console.WriteLine("PlaceOrder()");
                    Pause();
                    break;

                case "6":
                    Console.WriteLine("Returning to main menu.");
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Invalid option please try again.");
                    Pause();
                    break;
            }
        }
    }

    /*
    ==========================================================================================
    Method: Show a specific product from catalogue given id.
    ==========================================================================================
    */
    public void ShowProductById()
    {
        int productIdInput;
        Console.WriteLine("Enter the Id of the product that you want to show or press 'b' to go back to customer menu.");

        while (true)
        {
            string input = Console.ReadLine();

            if (input?.ToLower() == "b")
            {
                Console.WriteLine("Operation cancelled. Returning to customer menu...");
                return;
            }

            if (int.TryParse(input, out productIdInput))
                break;

            Console.WriteLine("Invalid input. Please enter a valid number or 'b' to go back:");
        }

        var productList = _productService.GetAllProducts();
        Product product = productList.FirstOrDefault(product => product.ProductId == productIdInput);
        Console.WriteLine(product);
        Pause();
    }

    /*
    ==========================================================================================
    Method: View cart for current customer.
    ==========================================================================================
    */
    public void ViewCartforCurrentCustomer()
{
    var cart = _customerService.GetOrCreateCart(_currentCustomerId);
    var cartItems = _customerService.GetCartItems(_currentCustomerId);

    Console.WriteLine("\n--- Your current cart ---");
    Console.WriteLine($"Cart OrderId: {cart.OrderId} | Status: {cart.OrderStatus}");

    if (cartItems.Count == 0)
    {
        Console.WriteLine("Your cart is empty.");
        Pause();
        return;
    }

    // Get the the productnames from catalogue by product id.
    var products = _productService.GetAllProducts();

    Console.WriteLine("\nItems:");
    foreach (var item in cartItems)
    {
        var product = products.FirstOrDefault(p => p.ProductId == item.ProductId);

        string productName = product != null ? product.ProductName : "(product not found)";
        double unitPrice = product != null ? product.ProductPrice : 0;

        Console.WriteLine($"- ProductId: {item.ProductId} | Name: {productName} | Product Price: {unitPrice} | Qty: {item.Amount} | Total Product price: ( {unitPrice} x {item.Amount} ) = {item.TotalPrice}");
    }

    Pause();
}




    /*
    ==========================================================================================
    Method: Add product to cart or create new CART from customer input. 
    ==========================================================================================
    */
    public void AddProductToCartFromInput()
    {
        Console.WriteLine("Enter Product ID:");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid product ID.");
            Pause();
            return;
        }

        Console.WriteLine("Enter quantity:");
        if (!int.TryParse(Console.ReadLine(), out int quantity))
        {
            Console.WriteLine("Invalid quantity.");
            Pause();
            return;
        }

        bool success = _customerService.AddProductToCart(
            _currentCustomerId,
            productId,
            quantity);

        if (success)
            Console.WriteLine("Product added to cart.");
        else
            Console.WriteLine("Failed to add product.");

        Pause();
    }

    /*
    ==========================================================================================
    Method: Reduce quantity of a product in CART or delete product from CART from customer input. 
    ==========================================================================================
    */

    public void RemoveProductFromCartFromInput()
    {
        Console.WriteLine("Enter Product ID (or 'b' to go back):");
        string productInput = Console.ReadLine();

        if (productInput?.ToLower() == "b")
        {
            Console.WriteLine("Operation cancelled. Returning to customer menu...");
            Pause();
            return;
        }

        if (!int.TryParse(productInput, out int productId))
        {
            Console.WriteLine("Invalid product ID.");
            Pause();
            return;
        }

        Console.WriteLine("Enter quantity to remove (or 'b' to go back):");
        string qtyInput = Console.ReadLine();

        if (qtyInput?.ToLower() == "b")
        {
            Console.WriteLine("Operation cancelled. Returning to customer menu...");
            Pause();
            return;
        }

        if (!int.TryParse(qtyInput, out int quantityToRemove) || quantityToRemove <= 0)
        {
            Console.WriteLine("Invalid quantity. Please enter a number > 0.");
            Pause();
            return;
        }

        bool success = _customerService.RemoveProductFromCart(
            _currentCustomerId,
            productId,
            quantityToRemove
        );

        if (success)
        {
            Console.WriteLine("Cart updated successfully.");
        }
        else
        {
            Console.WriteLine("Could not remove that quantity. (Product not in cart or quantity too high.)");
        }

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