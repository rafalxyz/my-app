using Microsoft.AspNetCore.Mvc;
using MyApp.Modules.Products.DTO;
using MyApp.Modules.Products.Services;

namespace MyApp.Web.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IProductImageService _productImageService;
    private readonly IProductReader _productReader;

    public ProductController(
        IProductService productService,
        IProductImageService productImageService,
        IProductReader productReader)
    {
        _productService = productService;
        _productImageService = productImageService;
        _productReader = productReader;
    }

    [HttpGet("{id}")]
    public IActionResult GetDetails(string id)
    {
        var details = _productReader.GetDetails(id);

        if (details == null)
        {
            return NotFound();
        }

        return new JsonResult(details);
    }

    [HttpGet("{id}/image")]
    public IActionResult GetImage(string id)
    {
        var image = _productImageService.GetImage(id);

        if (image == null)
        {
            return NotFound();
        }

        return File(image, "image/jpeg");
    }

    [HttpGet("search")]
    public IActionResult Search([FromQuery] ProductSearchDTO dto)
    {
        var response = _productReader.Search(dto);

        return new JsonResult(response);
    }

    [HttpPost("")]
    public IActionResult Create([FromForm] ProductCreateUpdateDTO dto)
    {
        var productId = _productService.Create(dto);

        var productDetails = _productReader.GetDetails(productId);

        return new CreatedAtActionResult(nameof(GetDetails), "Product", new { id = productId }, productDetails!);
    }

    [HttpPut("{id}")]
    public IActionResult Update(string id, [FromForm] ProductCreateUpdateDTO dto)
    {
        _productService.Update(id, dto);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        _productService.Delete(id);

        return NoContent();
    }

    [HttpDelete("")]
    public IActionResult DeleteMany([FromQuery]string[] ids)
    {
        _productService.DeleteMany(ids);

        return NoContent();
    }
}
