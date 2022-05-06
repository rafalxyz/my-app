using Microsoft.EntityFrameworkCore;
using MyApp.Modules.Subscriptions.Database.Entities;

namespace MyApp.Modules.Subscriptions.Database;

internal class SubscriptionsContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<ProductPriceSubscription> ProductPriceSubscriptions { get; set; } = null!;

    public SubscriptionsContext(DbContextOptions<SubscriptionsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("Subscriptions");
    }
}
