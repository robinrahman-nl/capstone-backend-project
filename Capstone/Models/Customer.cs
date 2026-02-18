using Capstone.Interfaces;

namespace Capstone.Models;

public class Customer : IDisplayable
{
    public User User { get; private set; }
    public int CustomerId { get; private set; }
    public int Age { get; private set; }


    public Customer(User user, int customerId, int age)
    {
        this.User = user;
        this.CustomerId = customerId;
        this.Age = age;
    }
    public virtual void DisplayDetails()
    {
        Console.WriteLine("User details");
        Console.WriteLine($"-\nUser ID: {User.UserId}\nFirst Name: {User.FirstName}\nLast Name: {User.LastName}\nUser Name: {User.UserName}\nUser Email: {User.UserEmail}\nUser Adress: {User.UserAdress}\nCustomer ID: {CustomerId}\nCustomer Age: {Age}");
    }

}