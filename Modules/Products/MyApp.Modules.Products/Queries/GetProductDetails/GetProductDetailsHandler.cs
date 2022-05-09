using MyApp.Modules.Products.Database;
using MyApp.Modules.Shared.Web;

namespace MyApp.Modules.Products.Queries.GetProductDetails;

internal class GetProductDetailsHandler
{
    private readonly ProductsContext _context;
    private readonly IUrlComposer _urlComposer;

    public GetProductDetailsHandler(ProductsContext context, IUrlComposer urlComposer)
    {
        _context = context;
        _urlComposer = urlComposer;
    }

    public ProductDetailsDTO? Handle(GetProductDetails query)
    {
        var product = _context.Products.Find(query.Id);

        if (product == null)
        {
            return null;
        }

        return new ProductDetailsDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Quantity = product.Quantity,
            IsActive = product.IsActive,
            ImageUrl = GetProductImageUrl(product.Id, product.ImageFileName)
        };
    }

    private string? GetProductImageUrl(string productId, string? imageFileName)
    {
        return !string.IsNullOrWhiteSpace(imageFileName)
            ? _urlComposer.Create($"api/products/{productId}/image")
            : null;
    }
}
