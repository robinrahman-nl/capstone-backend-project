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

        List <int> userIDList1 = userRepository1.GetAllUserID();

        User user2 = new User(userIDList1[0]);

        user2.DisplayDetails();
    }

    }

        






