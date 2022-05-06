using MyApp.Modules.Products.DTO;
using MyApp.Modules.Shared.Types;

namespace MyApp.Modules.Products.Services;

public interface IProductReader
{
    ProductDetailsDTO? GetDetails(string productId);

    PagedResponse<ProductItemDTO> Search(ProductSearchDTO dto);
}