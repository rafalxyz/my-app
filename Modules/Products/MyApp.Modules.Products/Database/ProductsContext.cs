using Microsoft.EntityFrameworkCore;
using MyApp.Modules.Products.Database.Entities;

namespace MyApp.Modules.Products.Database;

internal class ProductsContext : DbContext
{
    public DbSet<Product> Products { get; set; } = null!;

    public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("MyApp");
    }
}
