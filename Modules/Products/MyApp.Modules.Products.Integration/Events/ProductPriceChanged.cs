using MyApp.Modules.Shared.EventBus;

namespace MyApp.Modules.Products.Integration.Events;

public class ProductPriceChanged : IEvent
{
    public string ProductId { get; }

    public decimal PreviousPrice { get; }

    public decimal NewPrice { get; }

    public ProductPriceChanged(string productId, decimal previousPrice, decimal newPrice)
    {
        ProductId = productId;
        PreviousPrice = previousPrice;
        NewPrice = newPrice;
    }
}
