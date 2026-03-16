namespace Capstone.Models;

public class Admin : User
{
    public int AdminId { get; private set; }

    public Admin(
        int adminId,
        int userId,
        string firstName,
        string lastName,
        string userName,
        string userEmail,
        string userAddress
    ) : base(userId, firstName, lastName, userName, userEmail, userAddress)
    {
        AdminId = adminId;
    }


    public override string ToString()
    {
        return @$"
        ----------------
        Admin Details
        ----------------
        Admin Id: {AdminId}
        User Id: {UserId}
        First Name: {FirstName}
        Last Name: {LastName}
        User Name: {UserName}
        Email: {UserEmail}
        Adress: {UserAddress}
        ";
    }
}