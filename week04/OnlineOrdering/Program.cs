using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a physical or mailing address.
/// </summary>
public class Address
{
    private string street;
    private string city;
    private string stateProvince;
    private string country;

    public Address(string street, string city, string stateProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateProvince = stateProvince;
        this.country = country;
    }

    /// <summary>
    /// Determines whether the address is in the USA.
    /// </summary>
    public bool IsInUSA()
    {
        return country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Returns a formatted string containing the full address, each part on a new line.
    /// </summary>
    public string GetFullAddress()
    {
        return $"{street}\n{city}, {stateProvince}\n{country}";
    }
}

/// <summary>
/// Represents a customer with a name and an address.
/// </summary>
public class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string Name => name;

    public Address Address => address;

    /// <summary>
    /// Determines whether the customer lives in the USA (based on their address).
    /// </summary>
    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }
}

/// <summary>
/// Represents a product with a name, ID, unit price, and quantity.
/// </summary>
public class Product
{
    private string name;
    private string productId;
    private decimal price;      // price per unit
    private int quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public string Name => name;
    public string ProductId => productId;

    /// <summary>
    /// Calculates the total cost for this product line item (price * quantity).
    /// </summary>
    public decimal GetTotalCost()
    {
        return price * quantity;
    }
}

/// <summary>
/// Represents a customer order containing a list of products and a customer.
/// </summary>
public class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    /// <summary>
    /// Calculates the total price of the order: sum of product totals + shipping cost.
    /// Shipping is $5 for USA customers, $35 for international.
    /// </summary>
    public decimal GetTotalPrice()
    {
        decimal productsTotal = products.Sum(p => p.GetTotalCost());
        decimal shippingCost = customer.LivesInUSA() ? 5m : 35m;
        return productsTotal + shippingCost;
    }

    /// <summary>
    /// Returns a packing label string containing each product's name and product ID.
    /// </summary>
    public string GetPackingLabel()
    {
        return string.Join(Environment.NewLine,
            products.Select(p => $"{p.Name} (ID: {p.ProductId})"));
    }

    /// <summary>
    /// Returns a shipping label string containing the customer's name and full address.
    /// </summary>
    public string GetShippingLabel()
    {
        return $"{customer.Name}\n{customer.Address.GetFullAddress()}";
    }
}

/// <summary>
/// Main program that creates orders and displays their labels and total price.
/// </summary>
public class Program
{
    public static void Main()
    {
        // Create addresses
        Address usaAddress = new Address("123 Main St", "Springfield", "IL", "USA");
        Address canadaAddress = new Address("456 Queen St", "Toronto", "ON", "Canada");

        // Create customers
        Customer usaCustomer = new Customer("John Smith", usaAddress);
        Customer canadaCustomer = new Customer("Jane Doe", canadaAddress);

        // Create first order (USA customer) with 2 products
        Order order1 = new Order(usaCustomer);
        order1.AddProduct(new Product("Laptop", "A100", 799.99m, 1));
        order1.AddProduct(new Product("Mouse", "B205", 19.99m, 2));

        // Create second order (Canadian customer) with 3 products
        Order order2 = new Order(canadaCustomer);
        order2.AddProduct(new Product("Coffee Mug", "C300", 12.50m, 3));
        order2.AddProduct(new Product("Book", "D412", 18.99m, 1));
        order2.AddProduct(new Product("Pen Set", "E550", 7.25m, 2));

        // Display results for order 1
        Console.WriteLine("===== ORDER 1 =====");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order1.GetTotalPrice():F2}\n");

        // Display results for order 2
        Console.WriteLine("===== ORDER 2 =====");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order2.GetTotalPrice():F2}");
    }
}