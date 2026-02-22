namespace Capstone.Models;

public class Product
{
    public int ProductId  { get; set; }
    public string ProductName { get; private set; }
    public string Description { get; private set; }
    public double ProductPrice { get; private set; }
    public int QuantityInStock { get; private set; }

    public Product(int productId, string productName, string description, double productPrice, int quantityInStock)
    {
        ProductId = productId;
        ProductName = productName;
        Description = description;
        ProductPrice = productPrice;
        QuantityInStock = quantityInStock;
    }

    public Product(int productId)
    {
        ProductId = productId;
        ProductName = "Unknown";
        Description = "Unknown";
        ProductName = "Unknown";
        QuantityInStock = 0;
    }

    public override string ToString()
    {
        return @$"
        Product Id: {ProductId};
        Product Name: {ProductName};
        Description: {Description}
        ProductPrice: {ProductPrice};
        Quantity in Stock: {QuantityInStock};
        ";
    }
   
}