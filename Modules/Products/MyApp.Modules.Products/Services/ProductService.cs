using MyApp.Modules.Products.Database.Entities;
using MyApp.Modules.Products.Database;
using MyApp.Modules.Products.DTO;
using MyApp.Modules.Products.Integration.Events;
using MyApp.Modules.Shared.EventBus;
using MyApp.Modules.Shared.Exceptions;

namespace MyApp.Modules.Products.Services;

public interface IProductService
{
    string Create(ProductCreateUpdateDTO dto);

    void Delete(string productId);

    void DeleteMany(IEnumerable<string> productIds);

    void Update(string productId, ProductCreateUpdateDTO dto);
}

internal class ProductService : IProductService
{
    private readonly ProductsContext _context;
    private readonly IEventBus _eventBus;
    private readonly IProductImageService _productImageService;

    public ProductService(ProductsContext context, IEventBus eventBus, IProductImageService productImageService)
    {
        _context = context;
        _eventBus = eventBus;
        _productImageService = productImageService;
    }

    public string Create(ProductCreateUpdateDTO dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Price = dto.Price,
            Description = dto.Description,
            Quantity = dto.Quantity,
            IsActive = dto.IsActive,
            ImageFileName = dto.Image?.FileName
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        if (dto.Image != null)
        {
            _productImageService.UploadImage(dto.Image);
        }

        _eventBus.Publish(new ProductCreated(product.Id, product.Name));

        return product.Id;
    }

    public void Update(string productId, ProductCreateUpdateDTO dto)
    {
        var product = _context.Products.Find(productId)
            ?? throw new NotValidException("Product not found.");

        var originalPrice = product.Price;

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Description = dto.Description;
        product.Quantity = dto.Quantity;
        product.IsActive = dto.IsActive;
        product.ImageFileName = dto.Image?.FileName;

        _context.SaveChanges();

        if (dto.Image != null)
        {
            _productImageService.UploadImage(dto.Image);
        }

        if (product.Price != originalPrice)
        {
            _eventBus.Publish(new ProductPriceChanged(product.Id, originalPrice, product.Price));
        }
    }

    public void Delete(string productId)
    {
        var product = _context.Products.Find(productId)
            ?? throw new NotValidException("Product not found.");

        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    public void DeleteMany(IEnumerable<string> productIds)
    {
        var products = _context.Products.Where(x => productIds.Contains(x.Id));

        _context.Products.RemoveRange(products);
        _context.SaveChanges();
    }
}