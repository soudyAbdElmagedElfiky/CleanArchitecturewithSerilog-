namespace Products.Domain.Entities;

public sealed class Product
{
    private Product() { } // Required by EF Core

    public Product(string name, decimal price)
    {
        SetName(name);
        SetPrice(price);
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Price { get; private set; }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Product name is required.", nameof(name));

        Name = name.Trim();
    }

    private void SetPrice(decimal price)
    {
        if (price <= 0)
            throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than zero.");

        Price = price;
    }
}
