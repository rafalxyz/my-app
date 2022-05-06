namespace MyApp.Modules.Shared.EventBus;

public interface IEventHandler<TEvent> where TEvent : IEvent
{
    void Handle(TEvent message);
}
