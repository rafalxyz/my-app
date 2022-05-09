using MyApp.Modules.Products.Database;
using MyApp.Modules.Products.Integration.Events;
using MyApp.Modules.Products.Services;
using MyApp.Modules.Shared.EventBus;
using MyApp.Modules.Shared.Exceptions;

namespace MyApp.Modules.Products.Commands.UpdateProduct;

internal class UpdateProductHandler
{
    private readonly ProductsContext _context;
    private readonly IEventBus _eventBus;
    private readonly ProductImageService _productImageService;

    public UpdateProductHandler(ProductsContext context, IEventBus eventBus, ProductImageService productImageService)
    {
        _context = context;
        _eventBus = eventBus;
        _productImageService = productImageService;
    }

    public void Handle(UpdateProduct command)
    {
        var product = _context.Products.Find(command.Id)
            ?? throw new NotValidException("Product not found.");

        var originalPrice = product.Price;

        product.Name = command.Name;
        product.Price = command.Price;
        product.Description = command.Description;
        product.Quantity = command.Quantity;
        product.IsActive = command.IsActive;
        product.ImageFileName = command.Image?.FileName;

        _context.SaveChanges();

        if (command.Image != null)
        {
            _productImageService.UploadImage(command.Image);
        }

        if (product.Price != originalPrice)
        {
            _eventBus.Publish(new ProductPriceChanged(product.Id, originalPrice, product.Price));
        }
    }
}
