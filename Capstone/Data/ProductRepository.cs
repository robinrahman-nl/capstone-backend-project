using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class ProductRepository
{
    public readonly Database _database;

    public ProductRepository(Database database)
    {
        _database = database;
    }

    // ------------------------------------------------------------------
    // METHOD: Retrieves full Product objects.
    // ------------------------------------------------------------------

    public List<Product> GetAllProducts()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @$"
SELECT 
    product_id,
    product_name,
    description,
    product_price,
    quantity_in_stock
FROM
    product_catalogue
ORDER BY
    product_id;";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        List<Product> products = new List<Product>();
        while (reader.Read())
        {
            int productId = reader.GetInt32("product_id");
            string productName = reader.GetString("product_name");
            string description = reader.GetString("description");
            double productPrice = reader.GetDouble("product_price");
            int quantityInStock = reader.GetInt32("quantity_in_stock");

            Product product = new Product(productId, productName, description, productPrice, quantityInStock);
            products.Add(product);

        }
        return products;
    }

    /*
    Method: INSERT a new product in product catalogue. 
    */

    public int InsertProduct(string productName, string description, double productPrice, int quantityInStock)
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
        INSERT INTO 
        product_catalogue 
        (product_name, description, product_price, quantity_in_stock)
        VALUES
        (@product_name, @description, @product_price, @quantity_in_stock)
        ;";
        MySqlCommand myCommand = new MySqlCommand(query, connection);
        myCommand.Parameters.AddWithValue("@product_name", productName);
        myCommand.Parameters.AddWithValue("@description", description);
        myCommand.Parameters.AddWithValue("@product_price", productPrice);
        myCommand.Parameters.AddWithValue("@quantity_in_stock", quantityInStock);
        
        int affectedRows =  myCommand.ExecuteNonQuery();
        return affectedRows;
    }
}