using MyApp.Modules.Shared.Types;

namespace MyApp.Modules.Subscriptions.DTO;

public class SubscriptionSearchDTO : BaseSearchDTO
{
    public string? Email { get; set; }

    public DateTime? DateFrom { get; set; }

    public DateTime? DateTo { get; set; }

    public bool? IsActive { get; set; }
}
