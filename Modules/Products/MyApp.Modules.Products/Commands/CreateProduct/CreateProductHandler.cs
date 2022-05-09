using MyApp.Modules.Products.Database;
using MyApp.Modules.Products.Database.Entities;
using MyApp.Modules.Products.Integration.Events;
using MyApp.Modules.Products.Services;
using MyApp.Modules.Shared.EventBus;

namespace MyApp.Modules.Products.Commands.CreateProduct;

internal class CreateProductHandler
{
    private readonly ProductsContext _context;
    private readonly IEventBus _eventBus;
    private readonly ProductImageService _productImageService;

    public CreateProductHandler(ProductsContext context, IEventBus eventBus, ProductImageService productImageService)
    {
        _context = context;
        _eventBus = eventBus;
        _productImageService = productImageService;
    }

    public string Handle(CreateProduct command)
    {
        var product = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = command.Name,
            Price = command.Price,
            Description = command.Description,
            Quantity = command.Quantity,
            IsActive = command.IsActive,
            ImageFileName = command.Image?.FileName
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        if (command.Image != null)
        {
            _productImageService.UploadImage(command.Image);
        }

        _eventBus.Publish(new ProductCreated(product.Id, product.Name));

        return product.Id;
    }
}
