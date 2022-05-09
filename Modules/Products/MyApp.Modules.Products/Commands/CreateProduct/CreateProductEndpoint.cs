using FastEndpoints;

namespace MyApp.Modules.Products.Commands.CreateProduct;

internal class CreateProductEndpoint : Endpoint<CreateProduct>
{
    private readonly CreateProductHandler _handler;

    public CreateProductEndpoint(CreateProductHandler handler)
    {
        _handler = handler;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/api/products");
        AllowFormData();
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateProduct command, CancellationToken ct)
    {
        var productId = _handler.Handle(command);

        // TODO
        await SendCreatedAtAsync("/api/products/{id}", new { id = productId }, new { id = productId });
    }
}
