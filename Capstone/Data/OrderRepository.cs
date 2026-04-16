using System.IO.Pipelines;
using Capstone.Interfaces;
using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class OrderRepository : IOrderRepository
{
    public readonly Database _database;

    public OrderRepository(Database database_parameter)
    {
        _database = database_parameter;
    }

    /*
    ==================================================================================================
    METHOD: Retrieves full Order objects.
    ==================================================================================================
    */
    public List<Order> GetAllSubmittedOrders()
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

WHERE 
    o.order_status = 'SUBMITTED'

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

            Customer customer = new Customer(customerId, userId, firstName, lastName, userName, userEmail, userAddress, age);
            Order order = new Order(orderId, customer, orderDate, orderStatus);

            orders.Add(order);
        }

        return orders;
    }

    /*
    ==================================================================================================
    METHOD: Retrieve CART order for a specific customer.
    Returns null if no CART exists.
    ==================================================================================================
    */

    public Order? GetCartByCustomerId(int customerId)
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

FROM orders o

JOIN customer c 
    ON o.customer_id = c.customer_id

JOIN users u 
    ON c.user_id = u.user_id

WHERE 
    o.customer_id = @customer_id
    AND o.order_status = 'CART'

LIMIT 1;
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@customer_id", customerId);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        int orderId = reader.GetInt32("order_id");
        DateTime orderDate = reader.GetDateTime("order_date");
        string orderStatus = reader.GetString("order_status");

        int age = reader.GetInt32("age");

        int userId = reader.GetInt32("user_id");
        string firstName = reader.GetString("first_name");
        string lastName = reader.GetString("last_name");
        string userName = reader.GetString("user_name");
        string userEmail = reader.GetString("user_email");
        string userAddress = reader.GetString("user_address");

        Customer customer = new Customer(customerId, userId, firstName, lastName, userName, userEmail, userAddress, age);
        Order order = new Order(orderId, customer, orderDate, orderStatus);

        return order;
    }

    /*
    ==================================================================================================
    METHOD: Create new (empty) CART for customer by customer id, into 'orders' table.
    ==================================================================================================
    */
    public int CreateCart(int customerId)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
INSERT INTO orders (customer_id, order_date, order_status)
VALUES (@customer_id, NOW(), 'CART');
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@customer_id", customerId);

        int affectedRows = command.ExecuteNonQuery();

        return affectedRows;
    }

    /*
    ==================================================================================================
    // METHOD: Check if product already exists in CART
    ==================================================================================================
    */
    public OrderDetails? GetOrderDetail(int orderId, int productId)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
SELECT detail_id, amount, total_price
FROM order_details
WHERE order_id = @order_id
AND product_id = @product_id
LIMIT 1;
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@order_id", orderId);
        command.Parameters.AddWithValue("@product_id", productId);

        using var reader = command.ExecuteReader();

        if (!reader.Read())
            return null;

        int detailId = reader.GetInt32("detail_id");
        int amount = reader.GetInt32("amount");
        double totalPrice = reader.GetDouble("total_price");

        return new OrderDetails(detailId, orderId, productId, amount, totalPrice);
    }

    /*
    ==================================================================================================
    METHOD: Insert new product into order_details
    ==================================================================================================
    */
    public bool InsertOrderDetail(int orderId, int productId, int amount, double totalPrice)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
INSERT INTO order_details (order_id, product_id, amount, total_price)
VALUES (@order_id, @product_id, @amount, @total_price);
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@order_id", orderId);
        command.Parameters.AddWithValue("@product_id", productId);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@total_price", totalPrice);

        int rowsAffected = command.ExecuteNonQuery(); // ExecuteNonQuery() returns -1.

        return rowsAffected > 0; // returns false if rowsAffected = 0;
    }

    /*
    ==================================================================================================
    METHOD: Update existing order_detail amount and total_price.
    ==================================================================================================
    */
    public bool UpdateOrderDetail(int detailId, int newAmount, double newTotalPrice)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
UPDATE order_details
SET amount = @amount,
    total_price = @total_price
WHERE detail_id = @detail_id;
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@amount", newAmount);
        command.Parameters.AddWithValue("@total_price", newTotalPrice);
        command.Parameters.AddWithValue("@detail_id", detailId);

        int rowsAffected = command.ExecuteNonQuery(); // ExecuteNonQuery() returns -1. 
        return rowsAffected > 0; // returns false if rowsAffected = 0;
    }

    /*
    ==================================================================================================
    METHOD: Delete an order_details row by detail_id.
    Used when cart line amount becomes 0.
    ==================================================================================================
    */
    public bool DeleteOrderDetail(int detailId)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
DELETE FROM order_details
WHERE detail_id = @detail_id;
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@detail_id", detailId);

        int rowsAffected = command.ExecuteNonQuery();
        return rowsAffected > 0; // is false if rowsAffected = 0;
    }

    /*
    ==================================================================================================
    METHOD: Get all order_details rows for a specific order_id
    Used for showing cart contents (by customer or admin.
    ==================================================================================================
    */
    public List<OrderDetails> GetOrderDetailsByOrderId(int orderId)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
SELECT 
    detail_id,
    order_id,
    product_id,
    amount,
    total_price
FROM order_details
WHERE order_id = @order_id
ORDER BY detail_id;
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@order_id", orderId);

        using var reader = command.ExecuteReader();

        List<OrderDetails> details = new List<OrderDetails>();

        while (reader.Read())
        {
            int detailId = reader.GetInt32("detail_id");
            int productId = reader.GetInt32("product_id");
            int amount = reader.GetInt32("amount");
            double totalPrice = Convert.ToDouble(reader.GetDecimal("total_price"));

            details.Add(new OrderDetails(detailId, orderId, productId, amount, totalPrice));
        }

        return details;
    }

    /*
    ==================================================================================================
    METHOD: Update the status of an order ( CART -> PLACED or PLACED -> REJECTED).
    And set order_date to today's date when placing order.
    Returns: (int) Affected rows.
    ==================================================================================================
    */

    public int UpdateOrderStatus(int orderId, string newStatus)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
UPDATE orders
SET order_status = @status,
    order_date = CURDATE()
WHERE order_id = @order_id;
";

        using var command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@status", newStatus);
        command.Parameters.AddWithValue("@order_id", orderId);

        return command.ExecuteNonQuery();
    }


}