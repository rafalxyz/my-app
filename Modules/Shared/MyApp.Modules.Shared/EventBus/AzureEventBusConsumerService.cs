using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace MyApp.Modules.Shared.EventBus;

internal class AzureEventBusConsumerService : BackgroundService
{
    private readonly ServiceBusProcessor _serviceBusProcessor;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public AzureEventBusConsumerService(
        ServiceBusClient serviceBusClient,
        IServiceProvider serviceProvider,
        ILogger<AzureEventBusConsumerService> logger,
        IConfiguration configuration)
    {
        var options = new ServiceBusProcessorOptions
        {
            AutoCompleteMessages = false,
            MaxConcurrentCalls = 2
        };

        _serviceBusProcessor = serviceBusClient.CreateProcessor(
            configuration["ServiceBus:TopicName"],
            configuration["ServiceBus:SubscriptionName"],
            options);

        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _serviceBusProcessor.ProcessMessageAsync += MessageHandler;
        _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;

        _serviceBusProcessor.StartProcessingAsync().GetAwaiter().GetResult();

        return Task.CompletedTask;
    }

    private Task MessageHandler(ProcessMessageEventArgs args)
    {
        var messageType = Type.GetType($"{args.Message.ApplicationProperties["messageType"]}, {args.Message.ApplicationProperties["assemblyName"]}");

        var @event = JsonSerializer.Deserialize(Encoding.UTF8.GetString(args.Message.Body), messageType!);

        var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(new[] { @event!.GetType() });

        using (var scope = _serviceProvider.CreateScope())
        {
            var eventHandler = scope.ServiceProvider.GetRequiredService(eventHandlerType);

            eventHandler.GetType().GetMethod("Handle")!.Invoke(eventHandler, new object[] { @event });

            return args.CompleteMessageAsync(args.Message);
        }
    }

    private Task ErrorHandler(ProcessErrorEventArgs args)
    {
        _logger.LogError(args.Exception, "Error while processing message.");
        return Task.CompletedTask;
    }
}
