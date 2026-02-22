using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class OrderDetailsRepository
{
    public readonly Database _database;

    public OrderDetailsRepository(Database database)
    {
        _database = database;
    }

    // ------------------------------------------------------------
    // Get all order details (with Order + Product data)
    // ------------------------------------------------------------
    public List<OrderDetails> GetAllOrderDetails()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
SELECT 
    od.detail_id,
    od.order_id,
    od.product_id,
    od.amount,
    od.total_price,

    o.order_date,
    o.order_status,

    p.product_name,
    p.description,
    p.product_price,
    p.quantity_in_stock

FROM order_details od
JOIN orders o 
    ON od.order_id = o.order_id
JOIN product_catalogue p
    ON od.product_id = p.product_id
ORDER BY od.detail_id;
";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        List<OrderDetails> details = new List<OrderDetails>();

        while (reader.Read())
        {
            int detailId = reader.GetInt32("detail_id");

            int orderId = reader.GetInt32("order_id");
            DateTime orderDate = reader.GetDateTime("order_date");
            string orderStatus = reader.GetString("order_status");

            int productId = reader.GetInt32("product_id");
            string productName = reader.GetString("product_name");
            string description = reader.GetString("description");
            double productPrice = reader.GetDouble("product_price");
            int quantityInStock = reader.GetInt32("quantity_in_stock");

            int amount = reader.GetInt32("amount");
            double totalPrice = reader.GetDouble("total_price");

            // Minimal Order object (without customer for now)
            Order order = new Order(orderId, null, orderDate, orderStatus);

            Product product = new Product(productId, productName, description, productPrice, quantityInStock);

            OrderDetails orderDetail = new OrderDetails(detailId, order, product, amount, totalPrice);

            details.Add(orderDetail);
        }

        return details;
    }

}