using Microsoft.Extensions.Logging;
using MyApp.Modules.Products.Integration.Events;
using MyApp.Modules.Shared.Emails;
using MyApp.Modules.Shared.EventBus;
using MyApp.Modules.Subscriptions.Database;

namespace MyApp.Modules.Subscriptions.EventHandlers;

internal class ProductPriceChangedHandler : IEventHandler<ProductPriceChanged>
{
    private readonly SubscriptionsContext _context;
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;

    public ProductPriceChangedHandler(SubscriptionsContext context, IEmailSender emailSender, ILogger<ProductPriceChangedHandler> logger)
    {
        _context = context;
        _emailSender = emailSender;
        _logger = logger;
    }

    public void Handle(ProductPriceChanged message)
    {
        var subscriptions = _context.ProductPriceSubscriptions.Where(x => x.IsActive && x.ProductId == message.ProductId).ToList();

        var product = _context.Products.Find(message.ProductId)!;

        if (product == null)
        {
            _logger.LogError("Couldn't find product with ID: {ID}.", message.ProductId);
            return;
        }

        foreach (var subscription in subscriptions)
        {
            var email = new Email
            {
                Recipient = subscription.Email,
                Title = $"Price of product {product.Name} has changed.",
                Body = $@"
Price of product {product.Name} has changed.
Previous price was: {message.PreviousPrice}.
New price is: {message.NewPrice}."
            };

            _emailSender.Send(email);
        }
    }
}
