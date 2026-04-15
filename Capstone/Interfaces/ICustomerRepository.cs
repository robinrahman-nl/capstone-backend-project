using Capstone.Models;
namespace Capstone.Interfaces;
public interface ICustomerRepository
{
    List<Customer> GetAllCustomers();
}