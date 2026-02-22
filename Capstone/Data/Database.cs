using MySqlConnector;

namespace Capstone.Data;

public class Database
{
    private readonly string _connectionString = 
        "Server=localhost;Database=capstone_store;User=capstone_user;Password=1234;";

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
