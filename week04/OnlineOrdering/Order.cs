using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a customer order containing a list of products and a customer.
/// </summary>
public class Order
{
    private List<Product> _products;
    private Customer _customer;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public decimal GetTotalPrice()
    {
        decimal productsTotal = _products.Sum(p => p.GetTotalCost());
        decimal shippingCost = _customer.LivesInUSA() ? 5m : 35m;
        return productsTotal + shippingCost;
    }

    public string GetPackingLabel()
    {
        return string.Join(Environment.NewLine,
            _products.Select(p => $"{p.Name} (ID: {p.ProductId})"));
    }

    public string GetShippingLabel()
    {
        return $"{_customer.Name}\n{_customer.Address.GetFullAddress()}";
    }
}
