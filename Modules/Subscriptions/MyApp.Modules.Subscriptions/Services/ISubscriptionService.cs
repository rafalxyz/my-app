using MyApp.Modules.Shared.Types;
using MyApp.Modules.Subscriptions.DTO;

namespace MyApp.Modules.Subscriptions.Services;

public interface ISubscriptionService
{
    string Subscribe(SubscribeDTO dto);

    void Unsubscribe(string id);

    PagedResponse<SubscriptionItemDTO> Search(SubscriptionSearchDTO dto);
}