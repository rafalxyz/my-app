namespace MyApp.Modules.Products.Database.Entities;

public class Product
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public int Quantity { get; set; }

    public string? ImageFileName { get; set; }
}
