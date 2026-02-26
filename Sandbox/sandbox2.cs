/*
==================================================================
Test 1. in Program.cs: test tostring method for user object. 
Purpose: Validate User object behavior and ToString/DisplayDetails.
Also verify that UserRepository correctly retrieves
and maps database records into User objects.
==================================================================
*/
Database database1 = new Database();
UserRepository userRepository1 = new UserRepository(database1);

List<User> userList1 = userRepository1.GetAllUsers();

foreach (var item in userList1)
{
    Console.WriteLine(item);
}

/*
==================================================================
2. Test in Program.cs: 
Retrieve Admin objects from database via AdminRepository
and display formatted output.
==================================================================
*/
Database database1 = new Database();
UserRepository userRepository1 = new UserRepository(database1);

List<User> userList1 = userRepository1.GetAllUsers();

AdminRepository adminRepository1 = new AdminRepository(database1);
List<Admin> adminLIst1 = adminRepository1.GetAdmins();

foreach (var item in adminLIst1)
{
    Console.WriteLine(item);
}


// ==================================================================
// SANDBOX TEST 3
// Purpose: Retrieve Product entities from database via ProductRepository
// and verify correct object mapping and ToString() output formatting.
// Validates data pipeline: Database → Repository → Domain Model → Console.
// ==================================================================
Database database1 = new Database();
ProductRepository productRepository1 = new ProductRepository(database1);

List<Product> productsList1 = productRepository1.GetAllProducts();

foreach (var item in productsList1)
{
    Console.WriteLine(item);
}

// ==================================================================
// TEST 4: 
// Retrieve Customer entities from database
// Purpose: Validate CustomerRepository data retrieval and object mapping.
// Ensures joined User + Customer data is correctly constructed
// and formatted via Customer.ToString() output.
// Data flow: Database → CustomerRepository → Domain Model → Console.
// ==================================================================

Database database1 = new Database();
CustomerRepository customerRepository1 = new CustomerRepository(database1);

List<Customer> customerList1 = customerRepository1.GetAllCustomers();

foreach (var item in customerList1)
{
    Console.WriteLine(item);
}

// ==================================================================
// TEST 5: 
// Retrieve OrderDetails entities from database
// Purpose: Validate OrderDetailsRepository data retrieval,
// object mapping (Order + Product), and ToString() formatting.
// Data flow: Database → OrderDetailsRepository → Domain Model → Console.
// ==================================================================

Database database1 = new Database();
OrderDetailsRepository orderDetailsRepository1 = new OrderDetailsRepository(database1);

List<OrderDetails> orderDetailsList1 = orderDetailsRepository1.GetAllOrderDetails();

foreach (var item in orderDetailsList1)
{
    Console.WriteLine(item);
}

/*
####################################################################################################
Write in Database
####################################################################################################
*/

/* 
==================================================================
Source: StackOverflow
https://stackoverflow.com/questions/23301582/how-do-i-to-insert-data-into-an-sql-table-using-c-sharp-as-well-as-implement-an

==================================================================
*/




/* 
==================================================================
TEST nr: 
In program.cs
Repository-level INSERT validation
Verifies that InsertProduct() writes a new row to product_catalogue.
Hard coded.
==================================================================
*/

Database database1 = new Database();
ProductRepository productRepository3 = new ProductRepository(database1);

productRepository3.InsertProduct();

/* 
==================================================================
TEST nr: 
In program.cs
Repository-level INSERT validation
Verifies that InsertProduct() writes a new row to product_catalogue.
With parameters. 
==================================================================
*/

        Database database1 = new Database();
ProductRepository productRepository3 = new ProductRepository(database1);

productRepository3.InsertProduct("test4", "test4", 999, 0);

/* 
==================================================================
TEST nr: 
In program.cs
Validate AdminService.AddProduct() inserts a new product via ProductRepository
==================================================================
*/


        Database database1 = new Database();
ProductRepository productRepository3 = new ProductRepository(database1);
AdminService adminService3 = new AdminService(productRepository3);

adminService3.AddProduct("test5", "test5", 999, 0);

/* 
==================================================================
TEST nr: 
In program.cs
        
        TEST: End-to-End Add Product Flow (UI → Service → Repository)
        
        Purpose:
        - Manually validate full layered architecture.
        - Collect user input via console.
        - Pass data to AdminService.
        - Verify that ProductRepository inserts a new record into the database.
        
        Scope:
        - Integration-level test (not production UI yet).
        - Confirms correct wiring of dependencies and data flow.
     
==================================================================
*/
 Database database1 = new Database();
        ProductRepository productRepository3 = new ProductRepository(database1);
        AdminService adminService3 = new AdminService(productRepository3);
        UI uI3 = new UI(adminService3);
        


        Console.WriteLine("Enter product name.");
        string productNameInput = Console.ReadLine();

        Console.WriteLine("Enter product description");
        string descriptionInput = Console.ReadLine();

        double productPrice;

        while (true)
        {
            Console.WriteLine("Enter product price:");
            string input = Console.ReadLine();

            if (double.TryParse(input, out productPrice))
                break;

            Console.WriteLine("Invalid price. Please enter a valid number.");
        }

        int quantity;

        Console.WriteLine("Enter product quantity:");

        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.WriteLine("Invalid input. Please enter a valid number:");
        }



        adminService3.AddProduct(productNameInput, descriptionInput, productPrice, quantity);


