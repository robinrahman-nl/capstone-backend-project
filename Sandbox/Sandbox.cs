// using MySqlConnector;

string connectionString =
    "Server=localhost;Database=capstone_store;User=capstone_user;Password=1234;";

try
{
    using var connection = new MySqlConnection(connectionString);
    connection.Open();

    Console.WriteLine("Succesfully Connected to database!");

    string query = "SELECT product_name, product_price FROM product_catalogue;";

    using var command = new MySqlCommand(query, connection); 
    // Execute SELECT query and open a (forward-only) data stream
    using var reader = command.ExecuteReader(); 

// Iterate through each returned row
 while (reader.Read())
    {
    Console.WriteLine(reader.GetString("product_name"));
    }
}
catch (Exception ex)
{
    Console.WriteLine("❌ Error:");
    Console.WriteLine(ex.Message);
}


/* ########################################################################################
        Create an Test Object  
   ######################################################################################## */



string connectionString =
    "Server=localhost;Database=capstone_store;User=capstone_user;Password=1234;";

try
{
    using var connection = new MySqlConnection(connectionString);
    connection.Open();

    Console.WriteLine("Succesfully Connected to database!");

    string query = "SELECT user_id, first_name, last_name, user_name, user_email, user_address FROM users;";

    using var command = new MySqlCommand(query, connection); 
    // Execute SELECT query and open a (forward-only) data stream
    using var reader = command.ExecuteReader(); 

    //  !!!!!!!!!!!!! for testing !!!!!!!!!
    List<int> userIDList = new List<int>();

// Iterate through each returned row
 while (reader.Read())
    {
    Console.WriteLine(reader.GetInt32("user_id")); // test if getting the correct data. 
    int userID = reader.GetInt32("user_id");
    userIDList.Add(userID);
    }
foreach (int item in userIDList) // test if the filling the list is succesfull. 
{
 Console.WriteLine(item);   
}

// !!!!!!!!!!  Create a new User object !!!!!!!!!!
User user1 = new User(999);
user1.DisplayDetails();

User user2 = new User(userIDList[0]);
user2.DisplayDetails();
}

catch (Exception ex)
{
    Console.WriteLine("❌ Error:");
    Console.WriteLine(ex.Message);
}
    





