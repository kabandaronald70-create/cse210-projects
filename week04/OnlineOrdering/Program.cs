using System;
using System.Collections.Generic;
using System.Linq;

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

        Console.WriteLine("===== ONLINE ORDERING =====");

        Console.WriteLine("===== ORDER 1 =====");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order1.GetTotalPrice():F2}\n");

        Console.WriteLine("===== ORDER 2 =====");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine("\nShipping Label:");
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order2.GetTotalPrice():F2}");
    }
}
