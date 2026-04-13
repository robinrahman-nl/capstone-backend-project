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


    public int? GetCustomerIdByUserName(string username)
    {
        var customers = _customerRepository.GetAllCustomers();
        var customer = customers.FirstOrDefault(c => c.UserName.ToLower() == username.ToLower());

        if (customer == null)
        return null;


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
        Order? cart = _orderRepository.GetCartByCustomerId(customerId);

        if (cart != null)
            return cart;

        // If no cart exists → create one
        _orderRepository.CreateCart(customerId);

        // Retrieve newly created cart
        Order? newCart = _orderRepository.GetCartByCustomerId(customerId);

        if (newCart == null)
        {
            throw new Exception("Failed to create cart for customer.");
        }

        return newCart;
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
        OrderDetails? existingDetail =
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
        OrderDetails? existingDetail = _orderRepository.GetOrderDetail(cart.OrderId, productId);

        if (existingDetail == null)
            return false;

        // If user tries to remove more than exists -> fail
        if (quantityToRemove > existingDetail.Amount)
            return false;

        int newAmount = existingDetail.Amount - quantityToRemove;
        double newTotal = newAmount * unitPrice;

        // If amount becomes 0, delete the cart line; otherwise update amount and total.
        bool result;

        if (newAmount == 0)
        {
            result = _orderRepository.DeleteOrderDetail(existingDetail.DetailId);
        }
        else
        {
            result = _orderRepository.UpdateOrderDetail(existingDetail.DetailId, newAmount, newTotal);
        }

        return result;
    }

    public List<OrderDetails> GetCartItems(int customerId)
{
    // Check if cart exists and if not create cart.
    Order cart = GetOrCreateCart(customerId);

    // Fetch all lines for this cart
    return _orderRepository.GetOrderDetailsByOrderId(cart.OrderId);
}

/*
==========================================================================================
Method: Place order for current customer's CART
------------------------------------------------------------------------------------------
- If cart is empty -> return false
- If cart has items -> set status CART -> PLACED
- Then create a new empty CART so the cart is empty after placing the order
==========================================================================================
*/
public bool PlaceOrder(int customerId)
{
    // Ensure we have a CART
    Order cart = GetOrCreateCart(customerId);

    // Cart must have items before we can place an order
    List<OrderDetails> cartItems = _orderRepository.GetOrderDetailsByOrderId(cart.OrderId);
    if (cartItems.Count == 0)
        return false;

    // 1) Convert current cart into a placed order
    int updated = _orderRepository.UpdateOrderStatus(cart.OrderId, "PLACED");
    if (updated <= 0)
        return false;

    // 2) Create a fresh empty CART for the customer (Empty cart after placing order.)
    int created = _orderRepository.CreateCart(customerId);
    return created > 0;
}

}