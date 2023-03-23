using MyApp.Modules.Products.Database;
using MyApp.Modules.Products.DTO;
using MyApp.Modules.Shared.Extensions;
using MyApp.Modules.Shared.Types;
using MyApp.Modules.Shared.Web;

namespace MyApp.Modules.Products.Services;

public interface IProductReader
{
    ProductDetailsDTO? GetDetails(string productId);

    PagedResponse<ProductItemDTO> Search(ProductSearchDTO dto);
}

internal class ProductReader : IProductReader
{
    private readonly ProductsContext _context;
    private readonly IUrlComposer _urlComposer;

    public ProductReader(ProductsContext context, IUrlComposer urlComposer)
    {
        _context = context;
        _urlComposer = urlComposer;
    }

    public ProductDetailsDTO? GetDetails(string productId)
    {
        var product = _context.Products.Find(productId);

        if (product == null)
        {
            return null;
        }

        return new ProductDetailsDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            Quantity = product.Quantity,
            IsActive = product.IsActive,
            ImageUrl = GetProductImageUrl(product.Id, product.ImageFileName)
        };
    }

    private string? GetProductImageUrl(string productId, string? imageFileName)
    {
        return !string.IsNullOrWhiteSpace(imageFileName)
            ? _urlComposer.Create($"api/products/{productId}/image")
            : null;
    }

    public PagedResponse<ProductItemDTO> Search(ProductSearchDTO dto)
    {
        var query = _context.Products.Select(product => new ProductItemDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            IsActive = product.IsActive,
            ImageFileName = product.ImageFileName
        });

        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            query = query.Where(product => product.Name.Contains(dto.Name));
        }

        if (!string.IsNullOrWhiteSpace(dto.OrderBy))
        {
            query = (dto.OrderBy.ToPascalCase(), dto.OrderDirection) switch
            {
                (nameof(ProductItemDTO.Name), OrderDirection.Asc) => query.OrderBy(x => x.Name),
                (nameof(ProductItemDTO.Name), OrderDirection.Desc) => query.OrderByDescending(x => x.Name),
                (nameof(ProductItemDTO.Quantity), OrderDirection.Asc) => query.OrderBy(x => x.Quantity),
                (nameof(ProductItemDTO.Quantity), OrderDirection.Desc) => query.OrderByDescending(x => x.Quantity),
                (nameof(ProductItemDTO.Price), OrderDirection.Asc) => query.OrderBy(x => x.Price),
                (nameof(ProductItemDTO.Price), OrderDirection.Desc) => query.OrderByDescending(x => x.Price),
                (nameof(ProductItemDTO.IsActive), OrderDirection.Asc) => query.OrderBy(x => x.IsActive),
                (nameof(ProductItemDTO.IsActive), OrderDirection.Desc) => query.OrderByDescending(x => x.IsActive),
                _ => query.OrderBy(x => x.Id),
            };
        }

        var response = query.ToPagedResponse(dto);

        foreach (var item in response.Items)
        {
            item.ImageUrl = GetProductImageUrl(item.Id, item.ImageFileName);
        }

        return response;
    }
}