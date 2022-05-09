using MyApp.Modules.Products.Database;
using MyApp.Modules.Shared.Exceptions;

namespace MyApp.Modules.Products.Commands.DeleteProduct;

internal class DeleteProductHandler
{
    private readonly ProductsContext _context;

    public DeleteProductHandler(ProductsContext context)
    {
        _context = context;
    }

    public void Handle(DeleteProduct command)
    {
        var product = _context.Products.Find(command.Id)
            ?? throw new NotValidException("Product not found.");

        _context.Products.Remove(product);
        _context.SaveChanges();
    }
}
