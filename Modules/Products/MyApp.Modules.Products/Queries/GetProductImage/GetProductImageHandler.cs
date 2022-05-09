using MyApp.Modules.Products.Services;

namespace MyApp.Modules.Products.Queries.GetProductImage;

internal class GetProductImageHandler
{
    private readonly ProductImageService _productImageService;

    public GetProductImageHandler(ProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    public byte[]? Handle(GetProductImage query)
    {
        return _productImageService.GetImage(query.Id);
    }
}
