using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class CustomerRepository
{
    public readonly Database _database;

    public CustomerRepository(Database database)
    {
        this._database = database;
    }

    // ------------------------------------------------------------------
    // METHOD: Retrieves full Customer objects (Customer has User).
    // ------------------------------------------------------------------

    public List<Customer> GetAllCustomers()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
        SELECT
            c.customer_id,
            c.age,
            u.user_id,
            u.first_name,
            u.last_name,
            u.user_name,
            u.user_email,
            u.user_address
        FROM
            customer c
        JOIN
            users u
        ON
            c.user_id = u.user_id
        ORDER BY
            c.customer_id;
        ";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        List<Customer> customers = new List<Customer>();

        while (reader.Read())
        {
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

            customers.Add(customer);
        }

        return customers;
    }
}