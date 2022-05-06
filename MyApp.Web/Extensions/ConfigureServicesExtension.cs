using MyApp.Modules.Products;
using MyApp.Modules.Shared;

namespace MyApp.Web.Extensions;

public static class ConfigureServicesExtension
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddShared(configuration);
        services.AddSubscriptions(configuration);
        services.AddProducts(configuration);
    }
}
