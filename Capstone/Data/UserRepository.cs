
using System.Reflection.Metadata;
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

    // Method: Get all users user_id 
    public List<int> GetAllUserID()
    {
    List<int> userIDList = new List<int>();

    using var connection = _database.GetConnection();
    connection.Open();

    string query = "SELECT user_id, first_name, last_name, user_name, user_email, user_address FROM users;";
    using var command = new MySqlCommand(query, connection);
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        int userID = reader.GetInt32("user_id");
        userIDList.Add(userID);
    }
    return userIDList;
    }

    

}