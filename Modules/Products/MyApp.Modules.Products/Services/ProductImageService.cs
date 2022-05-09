using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MyApp.Modules.Products.Database;
using MyApp.Modules.Shared.Storage;

namespace MyApp.Modules.Products.Services;

internal class ProductImageService
{
    private readonly ProductsContext _context;
    private readonly IStorageService _storageService;
    private readonly IConfiguration _configuration;

    public ProductImageService(ProductsContext context, IStorageService storageService, IConfiguration configuration)
    {
        _context = context;
        _storageService = storageService;
        _configuration = configuration;
    }

    public void UploadImage(IFormFile file)
        => _storageService.Upload(DirectoryName, file);

    public byte[]? GetImage(string productId)
    {
        var product = _context.Products.Find(productId);

        if (product?.ImageFileName == null)
        {
            return null;
        }

        return _storageService.Download(DirectoryName, product.ImageFileName);
    }

    private string DirectoryName => _configuration["Storage:Directories:ProductPhotos"];
}
