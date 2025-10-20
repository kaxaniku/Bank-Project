using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Services.Interfaces.Services;

namespace Products.API.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public sealed class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductsController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateProduct([FromBody] Models.Product product)
    {
        if (product == null)
        {
            return BadRequest("Product is null");
        }

        _productService.AddProduct(_mapper.Map<DTO.Product>(product));
        return Ok(product);
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public IActionResult UpdatePhoto(int id, IFormFile photo)
    {
        var existingProduct = _productService.GetProduct(id);
        if (existingProduct == null)
        {
            return NotFound();
        }

        using (var memoryStream = new MemoryStream())
        {
            photo.CopyTo(memoryStream);
            existingProduct.Photo = memoryStream.ToArray();
        }

        _productService.EditProduct(existingProduct);
        return Ok(existingProduct);
    }

    [Authorize(Roles = "Admin")]
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
        return Ok(existingProduct);
    }
}