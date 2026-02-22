namespace Capstone.Models;

public class Customer
{
    public int CustomerId { get; private set; }
    public User User { get; private set; }
    public int Age { get; private set; }


    public Customer(int customerId, User user, int age)
    {
        this.CustomerId = customerId;
        this.User = user;
        this.Age = age;
    }
    public override string ToString()
    {
        return
    @"----------------
Customer Details
----------------
Customer ID: " + CustomerId + @"

" + User + @"

Age: " + Age + @"
";
    }

}