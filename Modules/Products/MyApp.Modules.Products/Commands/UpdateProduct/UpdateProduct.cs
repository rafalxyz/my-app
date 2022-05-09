using Microsoft.AspNetCore.Http;

namespace MyApp.Modules.Products.Commands.UpdateProduct;

internal class UpdateProduct
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public int Quantity { get; set; }

    public IFormFile? Image { get; set; }
}
