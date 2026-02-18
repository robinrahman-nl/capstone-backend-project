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
        return $"-\nUser ID: {User.UserId}\nFirst Name: {User.FirstName}\nLast Name: {User.LastName}\nUser Name: {User.UserName}\nUser Email: {User.UserEmail}\nUser Adress: {User.UserAdress}";
    }
}