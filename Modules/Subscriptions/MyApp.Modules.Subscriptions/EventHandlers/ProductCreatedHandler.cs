using MyApp.Modules.Products.Integration.Events;
using MyApp.Modules.Shared.EventBus;
using MyApp.Modules.Subscriptions.Database;
using MyApp.Modules.Subscriptions.Database.Entities;

namespace MyApp.Modules.Subscriptions.EventHandlers;

internal class ProductCreatedHandler : IEventHandler<ProductCreated>
{
    private readonly SubscriptionsContext _context;

    public ProductCreatedHandler(SubscriptionsContext context)
    {
        _context = context;
    }

    public void Handle(ProductCreated message)
    {
        var existingProduct = _context.Products.Find(message.Id);

        if (existingProduct != null)
        {
            return;
        }

        var product = new Product
        {
            Id = message.Id,
            Name = message.Name,
        };

        _context.Products.Add(product);
        _context.SaveChanges();
    }
}
