using Capstone.Data;
using Capstone.Models;


namespace Capstone;

    class Program {
    static void Main(string[] args)
    {
        
// ==================================================================
// TEST 5: 
// Retrieve OrderDetails entities from database
// Purpose: Validate OrderDetailsRepository data retrieval,
// object mapping (Order + Product), and ToString() formatting.
// Data flow: Database → OrderDetailsRepository → Domain Model → Console.
// ==================================================================

Database database1 = new Database();
OrderDetailsRepository orderDetailsRepository1 = new OrderDetailsRepository(database1);

List<OrderDetails> orderDetailsList1 = orderDetailsRepository1.GetAllOrderDetails();

foreach (var item in orderDetailsList1)
{
    Console.WriteLine(item);
}
    }
    }