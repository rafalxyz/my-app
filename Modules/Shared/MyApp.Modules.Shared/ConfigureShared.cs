using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Modules.Shared.DateTime;
using MyApp.Modules.Shared.Emails;
using MyApp.Modules.Shared.EventBus;
using MyApp.Modules.Shared.Storage;
using MyApp.Modules.Shared.Web;

namespace MyApp.Modules.Shared;

public static class ConfigureShared
{
    public static void AddShared(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(builder =>
        {
            builder.AddServiceBusClient(configuration["ServiceBus:ConnectionString"]);
        });

        services.AddSingleton(_ =>
            new BlobServiceClient(configuration["BlobStorage:ConnectionString"]));

        services.AddTransient<IEventBus, AzureEventBus>();
        services.AddTransient<IStorageService, StorageService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<IDateTime, AppDateTime>();
        services.AddTransient<IUrlComposer, UrlComposer>();

        services.AddHostedService<AzureEventBusConsumerService>();
    }
}
