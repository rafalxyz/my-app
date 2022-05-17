namespace MyApp.Modules.Products.DTO;

public class ProductItemDTO
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public int Quantity { get; set; }

    public string? ImageFileName { get; set; }

    public string? ImageUrl { get; set; }
}
