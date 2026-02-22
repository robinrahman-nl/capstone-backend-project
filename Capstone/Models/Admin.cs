namespace Capstone.Models;

public class Admin
{
    public User User { get; private set; }
    public int AdminId { get; private set; }

    public Admin(User user, int adminId)
    {
        User = user;
        AdminId = adminId;
    }


    public override string ToString()
    {
        return @$"
        ----------------
        Admin Details
        ----------------
        Admin Id: {AdminId}
        User Id: {User.UserId}
        First Name: {User.FirstName}
        Last Name: {User.LastName}
        User Name: {User.UserName}
        Email: {User.UserEmail}
        Adress: {User.UserAddress}
        ";
    }
}