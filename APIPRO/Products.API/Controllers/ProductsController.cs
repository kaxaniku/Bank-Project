using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Products.API.Models;
using Products.Services.Interfaces.Services;

namespace Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;   
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetProductById(int id)
    {
        var product = _productService.GetProduct(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Product product)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdatePhoto(int id, [FromBody] IFormFile photo)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}/price")]
    public IActionResult UpdatePrice(int id, [FromBody] decimal newPrice)
    {
        throw new NotImplementedException();
    }
}