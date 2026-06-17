using CFS.Application.Interfaces.Services;
using CFS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CFS.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts(CancellationToken ct)
    {
        var result = await productService.GetProductsAsync(ct);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(CancellationToken ct)
    {
        var product = new Product
        {
            Id = string.Empty,
            Name = "Sample Product",
            Description = "This is a sample product.",
            Price = 9.99m,
            Parameters = new Dictionary<string, string>
            {
                { "Color", "Red" },
                { "Size", "Medium" }
            }
        };

        var result = await productService.AddProductAsync(ct, product);
        return HandleResult(result);
    }
}
