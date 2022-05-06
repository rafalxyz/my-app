using MyApp.Modules.Shared.Types;

namespace MyApp.Modules.Products.DTO;

public class ProductSearchDTO : BaseSearchDTO
{
    public string? Name { get; set; }
}
