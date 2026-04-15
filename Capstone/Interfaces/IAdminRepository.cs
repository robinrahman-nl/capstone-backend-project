using Capstone.Models;
namespace Capstone.Interfaces;

public interface IAdminRepository
{
    List<Admin> GetAdmins();
}