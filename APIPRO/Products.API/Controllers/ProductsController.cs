using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Products.Services.Interfaces.Services;

namespace Products.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;   
    private readonly ILogger<ProductsController> _logger;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger, IMapper mapper)
    {
        _productService = productService;
        _logger = logger;
        _mapper = mapper;
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
    public IActionResult CreateProduct([FromBody] Models.Product product)
    {
        if (product == null)
        {
            return NotFound();
        }

        _productService.AddProduct(_mapper.Map<DTO.Product>(product));
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(int id, [FromBody] Models.Product product)
    {
        var existingProduct = _productService.GetProduct(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        var updatedProduct = _mapper.Map<DTO.Product>(product);
        updatedProduct.ProductId = id;

        _productService.EditProduct(updatedProduct);
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(int id)
    {
        var existingProduct = _productService.GetProduct(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        _productService.DeleteProduct(id);
        return Ok();
    }

    [HttpPatch("{id}")]
    public IActionResult UpdatePhoto(int id, [FromBody] IFormFile photo)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("{id}/price")]
    public IActionResult UpdatePrice(int id, [FromBody] decimal newPrice)
    {
        var existingProduct = _productService.GetProduct(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        existingProduct.Price = newPrice;
        _productService.EditProduct(existingProduct);
        return Ok($"{existingProduct.ProductId}, {existingProduct.ProductName}, {existingProduct.Price}, {existingProduct.Stock}, {existingProduct.Photo}");
    }
}