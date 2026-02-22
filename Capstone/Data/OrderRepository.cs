using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class OrderRepository
{
    public readonly Database _database;

    public OrderRepository(Database database_parameter)
    {
        _database = database_parameter;
    }

    // ------------------------------------------------------------------
    // METHOD: Retrieves full Order objects.
    // ------------------------------------------------------------------

    public List<Order> GetOrders()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
SELECT 
    o.order_id,
    o.order_date,
    o.order_status,

    c.customer_id,
    c.age,

    u.user_id,
    u.first_name,
    u.last_name,
    u.user_name,
    u.user_email,
    u.user_address

FROM 
    orders o

JOIN 
    customer c 
    ON o.customer_id = c.customer_id

JOIN 
    users u 
    ON c.user_id = u.user_id

ORDER BY 
    o.order_id;
";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        List<Order> orders = new List<Order>();

        while (reader.Read())
        {
            int orderId = reader.GetInt32("order_id");
            DateTime orderDate = reader.GetDateTime("order_date");
            string orderStatus = reader.GetString("order_status");

            int customerId = reader.GetInt32("customer_id");
            int age = reader.GetInt32("age");

            int userId = reader.GetInt32("user_id");
            string firstName = reader.GetString("first_name");
            string lastName = reader.GetString("last_name");
            string userName = reader.GetString("user_name");
            string userEmail = reader.GetString("user_email");
            string userAddress = reader.GetString("user_address");

            User user = new User(userId, firstName, lastName, userName, userEmail, userAddress);
            Customer customer = new Customer(customerId, user, age);
            Order order = new Order(orderId, customer, orderDate, orderStatus);

            orders.Add(order);
        }

        return orders;
    }
}