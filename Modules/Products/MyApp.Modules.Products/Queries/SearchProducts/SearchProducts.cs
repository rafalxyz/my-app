using MyApp.Modules.Shared.Types;

namespace MyApp.Modules.Products.Queries.SearchProducts;

public class SearchProducts : BaseSearchDTO
{
    public string? Name { get; set; }
}
