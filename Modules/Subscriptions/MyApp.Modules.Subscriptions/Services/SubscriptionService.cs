using MyApp.Modules.Shared.DateTime;
using MyApp.Modules.Shared.Exceptions;
using MyApp.Modules.Shared.Extensions;
using MyApp.Modules.Shared.Types;
using MyApp.Modules.Subscriptions.Database;
using MyApp.Modules.Subscriptions.Database.Entities;
using MyApp.Modules.Subscriptions.DTO;

namespace MyApp.Modules.Subscriptions.Services;

internal class SubscriptionService : ISubscriptionService
{
    private readonly SubscriptionsContext _context;
    private readonly IDateTime _dateTime;

    public SubscriptionService(SubscriptionsContext context, IDateTime dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public string Subscribe(SubscribeDTO dto)
    {
        var activeSubscription = GetActiveSubscription(dto.Email, dto.ProductId);

        if (activeSubscription != null)
        {
            throw new NotValidException("Subscription is already active.");
        }

        var subscription = new ProductPriceSubscription
        {
            Id = Guid.NewGuid().ToString(),
            Email = dto.Email,
            ProductId = dto.ProductId,
            IsActive = true,
            CreatedAt = _dateTime.Now()
        };

        _context.ProductPriceSubscriptions.Add(subscription);

        _context.SaveChanges();

        return subscription.Id;
    }

    public void Unsubscribe(string subscriptionId)
    {
        var activeSubscription = _context.ProductPriceSubscriptions.Find(subscriptionId);

        if (activeSubscription == null || !activeSubscription.IsActive)
        {
            throw new NotValidException("Subscription does not exist or is not active.");
        }

        activeSubscription.IsActive = false;

        _context.SaveChanges();
    }

    public PagedResponse<SubscriptionItemDTO> Search(SubscriptionSearchDTO dto)
    {
        var query = _context.ProductPriceSubscriptions.Select(subscription => new SubscriptionItemDTO
        {
            Id = subscription.Id,
            Email = subscription.Email,
            ProductId = subscription.ProductId,
            CreatedAt = subscription.CreatedAt,
            IsActive = subscription.IsActive,
        });

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            query = query.Where(subscription => subscription.Email.Contains(dto.Email));
        }

        if (dto.IsActive == true)
        {
            query = query.Where(subscription => subscription.IsActive);
        }

        if (dto.DateFrom.HasValue)
        {
            query = query.Where(subscription => subscription.CreatedAt >= dto.DateFrom.Value);
        }

        if (dto.DateTo.HasValue)
        {
            query = query.Where(subscription => subscription.CreatedAt <= dto.DateTo.Value.AddDays(1));
        }

        if (!string.IsNullOrWhiteSpace(dto.OrderBy))
        {
            query = (dto.OrderBy.ToPascalCase(), dto.OrderDirection) switch
            {
                (nameof(SubscriptionItemDTO.Email), OrderDirection.Asc) => query.OrderBy(x => x.Email),
                (nameof(SubscriptionItemDTO.Email), OrderDirection.Desc) => query.OrderByDescending(x => x.Email),
                (nameof(SubscriptionItemDTO.CreatedAt), OrderDirection.Asc) => query.OrderBy(x => x.CreatedAt),
                (nameof(SubscriptionItemDTO.CreatedAt), OrderDirection.Desc) => query.OrderByDescending(x => x.CreatedAt),
                (nameof(SubscriptionItemDTO.IsActive), OrderDirection.Asc) => query.OrderBy(x => x.IsActive),
                (nameof(SubscriptionItemDTO.IsActive), OrderDirection.Desc) => query.OrderByDescending(x => x.IsActive),
                _ => query.OrderBy(x => x.Id),
            };
        }

        var response = query.ToPagedResponse(dto);

        var products = _context.Products.Where(product => response.Items.Select(item => item.ProductId).Contains(product.Id)).ToList();

        foreach (var item in response.Items)
        {
            item.ProductName = products.SingleOrDefault(x => x.Id == item.ProductId)?.Name;
        }

        return response;
    }

    private ProductPriceSubscription? GetActiveSubscription(string email, string productId)
    {
        return _context.ProductPriceSubscriptions
            .SingleOrDefault(x => x.Email == email && x.ProductId == productId && x.IsActive);
    }
}
