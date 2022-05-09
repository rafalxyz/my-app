using FastEndpoints;

namespace MyApp.Modules.Products.Queries.GetProductDetails;

internal class GetProductDetailsEndpoint : Endpoint<GetProductDetails>
{
    private readonly GetProductDetailsHandler _handler;

    public GetProductDetailsEndpoint(GetProductDetailsHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductDetails query, CancellationToken ct)
    {
        var details = _handler.Handle(query);

        if (details == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(details);
    }
}

