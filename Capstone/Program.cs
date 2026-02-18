using Capstone.Data;
using Capstone.Models;


namespace Capstone;

    class Program {
    static void Main(string[] args)
    {
        User user1 = new User(999);
        user1.DisplayDetails();

        // #######################
        Database database1 = new Database();
        UserRepository userRepository1 = new UserRepository(database1);

        List <User> userList1 = userRepository1.GetAllUsers();

        foreach (var item in userList1)
        {
            Console.WriteLine(item);
        }
    }

    }

