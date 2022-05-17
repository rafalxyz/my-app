using MyApp.Modules.Products.DTO;

namespace MyApp.Modules.Products.Services;

public interface IProductService
{
    string Create(ProductCreateUpdateDTO dto);

    void Delete(string productId);

    void DeleteMany(IEnumerable<string> productIds);

    void Update(string productId, ProductCreateUpdateDTO dto);
}