using Capstone.Models;
using MySqlConnector;

namespace Capstone.Data;

public class AdminRepository
{
    public readonly Database _database;

    public AdminRepository(Database database_parameter)
    {
        this._database = database_parameter;
    }

    // ------------------------------------------------------------------
    // METHOD: Retrieves full Admin objects.
    // ------------------------------------------------------------------

    public List<Admin> GetAdmins()
    {
        using var connection = _database.GetConnection();
        connection.Open();

        string query = @"
    SELECT 
        u.user_id,
        u.first_name,
        u.last_name,
        u.user_name,
        u.user_email,
        u.user_address,
        a.admin_id
    FROM 
        admin a
    JOIN 
        users u 
    ON 
        a.user_id = u.user_id;";

        using var command = new MySqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        List<Admin> admins = new List<Admin>();
        while (reader.Read())
        {
            int userId = reader.GetInt32("user_id");
            string firstName = reader.GetString("first_name");
            string lastName = reader.GetString("last_name");
            string userName = reader.GetString("user_name");
            string userEmail = reader.GetString("user_email");
            string address = reader.GetString("user_address");
            int adminId = reader.GetInt32("admin_id");

            User user = new User(userId, firstName, lastName, userName, userEmail, address);
            Admin admin = new Admin(user, adminId);

            admins.Add(admin);
        }
        return admins;

    }

}
// check sql query for admin table.
// look at admin constructor. how to create new admin object? 
// new Admin(User user, admin_id) ?