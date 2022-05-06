using MyApp.Modules.Shared.EventBus;

namespace MyApp.Modules.Products.Integration.Events;

public class ProductCreated : IEvent
{
    public string Id { get; }

    public string Name { get; }

    public ProductCreated(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
