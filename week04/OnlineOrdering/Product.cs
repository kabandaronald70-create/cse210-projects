/// <summary>
/// Represents a product with a name, ID, unit price, and quantity.
/// </summary>
public class Product
{
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    public Product(string name, string productId, decimal price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string Name => _name;
    public string ProductId => _productId;

    public decimal GetTotalCost()
    {
        return _price * _quantity;
    }
}
