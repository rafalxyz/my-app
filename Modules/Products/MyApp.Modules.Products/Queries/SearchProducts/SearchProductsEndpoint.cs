using FastEndpoints;

namespace MyApp.Modules.Products.Queries.SearchProducts;

internal class SearchProductsEndpoint : Endpoint<SearchProducts>
{
    private readonly SearchProductsHandler _handler;

    public SearchProductsEndpoint(SearchProductsHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/products/search");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SearchProducts query, CancellationToken ct)
    {
        var data = _handler.Handle(query);

        await SendAsync(data);
    }
}

