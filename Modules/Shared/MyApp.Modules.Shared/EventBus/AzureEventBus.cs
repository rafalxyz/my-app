using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace MyApp.Modules.Shared.EventBus;

internal class AzureEventBus : IEventBus
{
    private readonly ServiceBusSender _serviceBusSender;

    public AzureEventBus(ServiceBusClient serviceBusClient, IConfiguration configuration)
    {
        _serviceBusSender = serviceBusClient.CreateSender(configuration["ServiceBus:TopicName"]);
    }

    public void Publish<T>(T @event) where T : IEvent
    {
        var text = JsonSerializer.Serialize(@event);

        var message = new ServiceBusMessage(Encoding.UTF8.GetBytes(text));

        message.ApplicationProperties["messageType"] = typeof(T).FullName;
        message.ApplicationProperties["assemblyName"] = typeof(T).Assembly.GetName().Name;

        _serviceBusSender.SendMessageAsync(message).GetAwaiter().GetResult();
    }
}
