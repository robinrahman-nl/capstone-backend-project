using Capstone.Data;
using Capstone.Models;
using Capstone.Interfaces;
using System.Linq;
namespace Capstone.Services;

public class CustomerService : ICustomerService
{
    public readonly ProductRepository _productRepository;
    public readonly CustomerRepository _customerRepository;
    public readonly OrderRepository _orderRepository;

    public CustomerService(ProductRepository productRepository, CustomerRepository customerRepository, OrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }


    public int GetCustomerIdByUserName(string username)
    {
        var customers = _customerRepository.GetAllCustomers();
        var customer = customers.FirstOrDefault(c => c.UserName.ToLower() == username.ToLower());

        if (customer == null)

            return -1;


        return customer.CustomerId;
    }


    /*
    ==========================================================================================
    Method: Get existing CART order for customer or create one if co CART exists for this customer id.
    ------------------------------------------------------------------------------------------
    - Customer always have an active CART before products can be added.
    - If a CART already exists → return CART.
    - If no CART exists for this customer id → create a new CART and return that CART.
    ==========================================================================================
    */
    public Order GetOrCreateCart(int customerId)
    {
        Order cart = _orderRepository.GetCartByCustomerId(customerId);

        if (cart != null)
            return cart;

        // If no cart exists → create one
        _orderRepository.CreateCart(customerId);

        // Retrieve newly created cart
        return _orderRepository.GetCartByCustomerId(customerId);
    }

    /*
    ==========================================================================================
    Method: Add product to customer's CART
    ==========================================================================================
    */
    public bool AddProductToCart(int customerId, int productId, int quantity)
    {
        // Check if CART exists
        Order cart = GetOrCreateCart(customerId);

        // Get product by product id. 
        var products = _productRepository.GetAllProducts();
        var product = products.FirstOrDefault(p => p.ProductId == productId);

        if (product == null)
            return false;

        double unitPrice = product.ProductPrice;

        // Check if product exist already in cart. Else create new CART (order_detal row) in order_details table. 
        OrderDetails existingDetail =
            _orderRepository.GetOrderDetail(cart.OrderId, productId);

        if (existingDetail != null)
        {
            int newAmount = existingDetail.Amount + quantity;
            double newTotal = newAmount * unitPrice;

            _orderRepository.UpdateOrderDetail(
                existingDetail.DetailId,
                newAmount,
                newTotal);
        }
        else
        {
            double totalPrice = quantity * unitPrice;

            _orderRepository.InsertOrderDetail(
                cart.OrderId,
                productId,
                quantity,
                totalPrice);
        }

        return true;
    }

    /*
    ==========================================================================================
    Method: Remove product from customer's CART
    ------------------------------------------------------------------------------------------
    - If product is not in cart -> return false
    - If quantityToRemove > current amount -> return false
    - If quantityToRemove < current amount -> decrease amount and update total
    - If quantityToRemove == current amount -> set amount/total to 0 (placeholder for delete)
    ==========================================================================================
    */
    public bool RemoveProductFromCart(int customerId, int productId, int quantityToRemove)
    {
        if (quantityToRemove <= 0)
            return false;

        // Ensure CART exists (customer always operates on an active CART)
        Order cart = GetOrCreateCart(customerId);

        // Find the product for recomputing totals
        var products = _productRepository.GetAllProducts();
        var product = products.FirstOrDefault(p => p.ProductId == productId);

        if (product == null)
            return false;

        double unitPrice = product.ProductPrice;

        // Find existing cart line
        OrderDetails existingDetail = _orderRepository.GetOrderDetail(cart.OrderId, productId);

        if (existingDetail == null)
            return false;

        // If user tries to remove more than exists -> fail
        if (quantityToRemove > existingDetail.Amount)
            return false;

        int newAmount = existingDetail.Amount - quantityToRemove;
        double newTotal = newAmount * unitPrice;

        // If amount becomes 0, delete the cart line; otherwise update amount and total.
        int result;

        if (newAmount == 0)
        {
            result = _orderRepository.DeleteOrderDetail(existingDetail.DetailId);
        }
        else
        {
            result = _orderRepository.UpdateOrderDetail(existingDetail.DetailId, newAmount, newTotal);
        }

        return result > 0;
    }

    public List<OrderDetails> GetCartItems(int customerId)
{
    // Check if cart exists and if not create cart.
    Order cart = GetOrCreateCart(customerId);

    // Fetch all lines for this cart
    return _orderRepository.GetOrderDetailsByOrderId(cart.OrderId);
}


}