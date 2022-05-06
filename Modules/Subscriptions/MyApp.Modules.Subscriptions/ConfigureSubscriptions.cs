using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Modules.Products.Integration.Events;
using MyApp.Modules.Shared.EventBus;
using MyApp.Modules.Subscriptions.Database;
using MyApp.Modules.Subscriptions.EventHandlers;
using MyApp.Modules.Subscriptions.Services;

namespace MyApp.Modules.Shared;

public static class ConfigureSubscriptions
{
    public static void AddSubscriptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SubscriptionsContext>(options =>
        {
            options.UseCosmos(
                configuration["CosmosDb:Endpoint"],
                configuration["CosmosDb:AccountKey"],
                configuration["CosmosDb:DatabaseName"]);
        });

        services.AddTransient<IEventHandler<ProductPriceChanged>, ProductPriceChangedHandler>();
        services.AddTransient<IEventHandler<ProductCreated>, ProductCreatedHandler>();
        services.AddTransient<ISubscriptionService, SubscriptionService>();
    }

    public static void InitSubscriptions(this IServiceProvider serviceProvider, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var context = serviceProvider.GetRequiredService<SubscriptionsContext>();

            context.Database.EnsureCreated();
        }
    }
}
