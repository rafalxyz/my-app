using Bogus;
using MyApp.Modules.Products.Database.Entities;

namespace MyApp.Modules.Products.Database;

internal class ProductSeeder
{
    private const int Count = 200;

    private readonly ProductsContext _context;

    public ProductSeeder(ProductsContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        var isAnyProductAlreadyAdded = _context.Products.Count() > 0;

        if (isAnyProductAlreadyAdded)
        {
            return;
        }

        var products = Generate();

        _context.Products.AddRange(products);
        _context.SaveChanges();
    }

    private static IEnumerable<Product> Generate()
    {
        return new Faker<Product>()
            .RuleFor(p => p.Id, f => f.Random.Guid().ToString())
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Lorem.Paragraphs(2))
            .RuleFor(p => p.Price, f => f.Random.Number(100 * 100, 1_000_000 * 100) / 100m)
            .RuleFor(p => p.Quantity, f => f.Random.Number(1_000))
            .RuleFor(p => p.IsActive, f => f.Random.Bool(0.9f))
            .Generate(Count);
    }
}
