using FastEndpoints;

namespace MyApp.Modules.Products.Commands.UpdateProduct;

internal class UpdateProductEndpoint : Endpoint<UpdateProduct>
{
    private readonly UpdateProductHandler _handler;

    public UpdateProductEndpoint(UpdateProductHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Verbs(Http.PUT);
        Routes("/api/products/{id}");
        AllowFormData();
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProduct command, CancellationToken ct)
    {
        _handler.Handle(command);

        await SendNoContentAsync();
    }
}
