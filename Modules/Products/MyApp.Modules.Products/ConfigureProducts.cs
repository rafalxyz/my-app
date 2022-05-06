using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.Modules.Products.Database;
using MyApp.Modules.Products.Services;

namespace MyApp.Modules.Products;

public static class ConfigureProducts
{
    public static void AddProducts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductsContext>(options =>
        {
            options.UseCosmos(
                configuration["CosmosDb:Endpoint"],
                configuration["CosmosDb:AccountKey"],
                configuration["CosmosDb:DatabaseName"]);
        });

        services.AddTransient<IProductService, ProductService>();
        services.AddTransient<IProductReader, ProductReader>();
        services.AddTransient<IProductImageService, ProductImageService>();
        services.AddTransient<ProductSeeder>();
    }

    public static void InitProducts(this IServiceProvider serviceProvider, IHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            var context = serviceProvider.GetRequiredService<ProductsContext>();

            context.Database.EnsureCreated();
        }
    }
}
