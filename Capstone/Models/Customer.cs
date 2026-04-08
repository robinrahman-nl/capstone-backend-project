namespace Capstone.Models;

public class Customer : User
{
    public int CustomerId { get; private set; }
    public int Age { get; private set; }


    public Customer(
        int customerId,
        int userId,
        string firstName,
        string lastName,
        string userName,
        string userEmail,
        string userAddress,
        int age
        ) : base(userId, firstName, lastName, userName, userEmail, userAddress)
    {
        this.CustomerId = customerId;

        this.Age = age;
    }

    public Customer(int customerId)
    {
        CustomerId = customerId;
    }
    public override string ToString()
    {
        return
    $@"----------------
Customer Details
----------------
Customer ID: {CustomerId}

User ID: {UserId}
First Name: {FirstName}
Last Name: {LastName}
User Name: {UserName}
User Email: {UserEmail}
User Address: {UserAddress}
Age: {Age}
";
    }

}