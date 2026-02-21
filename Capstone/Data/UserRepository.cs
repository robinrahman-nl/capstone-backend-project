using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class UserRepository
{
    public readonly Database _database;

    // Constructor
    public UserRepository(Database database_parameter)
    {
        this._database = database_parameter;
    }

    // -------------------------------------------------------------------
    // Method:  Retrieves only user IDs to demonstrate data pipeline flow.
    // -------------------------------------------------------------------
    public List<int> GetAllUserID()
    {
    List<int> userIDList = new List<int>();

    using var connection = _database.GetConnection();
    connection.Open();

    string query = "SELECT user_id FROM users;";
    using var command = new MySqlCommand(query, connection);
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        int userID = reader.GetInt32("user_id");
        userIDList.Add(userID);
    }
    return userIDList;
    }
    
    // ------------------------------------------------------------------
    // METHOD: Retrieves full User objects.
    // ------------------------------------------------------------------
    
    public List<User> GetAllUsers()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
    SELECT 
        user_id,
        first_name,
        last_name, 
        user_name, 
        user_email, 
        user_address 
    FROM 
        users
    ORDER BY
        user_id";
        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        List<User> users = new List<User>();
        while (reader.Read())
        {
            int userId = reader.GetInt32("user_id");
            string firstName = reader.GetString("first_name");
            string lastName = reader.GetString("last_name");
            string userName = reader.GetString("user_name");
            string userEmail = reader.GetString("user_email");
            string address = reader.GetString("user_address");

            User user = new User(userId, firstName, lastName, userName, userEmail, address);
            users.Add(user);
        }
        return users;
    }
 }