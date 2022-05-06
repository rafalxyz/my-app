namespace MyApp.Modules.Subscriptions.Database.Entities;

public class ProductPriceSubscription
{
    public string Id { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}
