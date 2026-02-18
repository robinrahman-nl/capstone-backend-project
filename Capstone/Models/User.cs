using Capstone.Interfaces;

namespace Capstone.Models;

public class User : IDisplayable
{
    public int UserId { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string UserName { get; private set; }
    public string UserEmail { get; private set; }
    public string UserAdress { get; private set; }

    public User(int userId, string firstName, string lastName, string userName, string userEmail, string userAdress)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        UserEmail = userEmail;
        UserAdress = userAdress;
    }

    public User(int userId)
    {
        UserId = userId;
        FirstName = "Unknown";
        LastName = "Unknown";
        UserName = "Unknown";
        UserEmail = "Unknown";
        UserAdress = "Unknown";
    }

    public User()
    {
        UserId = 0;
        FirstName = "Unknown";
        LastName = "Unknown";
        UserName = "Unknown";
        UserEmail = "Unknown";
        UserAdress = "Unknown";
    }

    // Prints the details of the user to the console.
    public virtual void DisplayDetails()
    {
        Console.WriteLine("User details");
        Console.WriteLine($"-\nUser ID: {UserId}\nFirst Name: {FirstName}\nLast Name: {LastName}\nUser Name: {UserName}\nUser Email: {UserEmail}\nUser Adress: {UserAdress}");
    }

}