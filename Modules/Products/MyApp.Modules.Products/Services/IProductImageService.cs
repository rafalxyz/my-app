using Microsoft.AspNetCore.Http;

namespace MyApp.Modules.Products.Services;

public interface IProductImageService
{
    void UploadImage(IFormFile file);

    byte[]? GetImage(string productId);
}