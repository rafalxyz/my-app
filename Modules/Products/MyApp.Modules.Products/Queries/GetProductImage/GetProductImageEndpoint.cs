using FastEndpoints;

namespace MyApp.Modules.Products.Queries.GetProductImage;

internal class GetProductImageEndpoint : Endpoint<GetProductImage>
{
    private readonly GetProductImageHandler _handler;

    public GetProductImageEndpoint(GetProductImageHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("/api/products/{id}/image");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductImage query, CancellationToken ct)
    {
        var bytes = _handler.Handle(query);

        if (bytes == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendBytesAsync(bytes, contentType: "image/jpeg");
    }
}

