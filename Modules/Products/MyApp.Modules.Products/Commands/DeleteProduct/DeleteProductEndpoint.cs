using FastEndpoints;

namespace MyApp.Modules.Products.Commands.DeleteProduct;

internal class DeleteProductEndpoint : Endpoint<DeleteProduct>
{
    private readonly DeleteProductHandler _handler;

    public DeleteProductEndpoint(DeleteProductHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Verbs(Http.DELETE);
        Routes("/api/products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteProduct command, CancellationToken ct)
    {
        _handler.Handle(command);

        await SendNoContentAsync();
    }
}
