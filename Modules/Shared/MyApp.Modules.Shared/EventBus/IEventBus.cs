namespace MyApp.Modules.Shared.EventBus;

public interface IEventBus
{
    void Publish<T>(T message) where T : IEvent;
}
