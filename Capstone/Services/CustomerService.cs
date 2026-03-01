using Capstone.Data;
using Capstone.Models;
namespace Capstone.Services;

public class CustomerService
{
    public readonly ProductRepository _productRepository;
    public readonly CustomerRepository _customerRepository;

    public CustomerService(ProductRepository productRepository, CustomerRepository customerRepository)
    {
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }


     public int getCustomerIdByUserName(string username)
    {
        var customers = _customerRepository.GetAllCustomers();
        var customer =  customers.FirstOrDefault(c => c.User.UserName.ToLower() == username.ToLower());

        if (customer == null)
                 
            return -1;    
        

        return customer.CustomerId;
    }
}