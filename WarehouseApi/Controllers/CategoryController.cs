using Microsoft.AspNetCore.Mvc;
using Warehouse.DTO;
using Warehouse.Services.Interfaces.Services;
using WarehouseApi.Models;

namespace WarehouseApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class CategoryController(IProductService productService) : ControllerBase
{
    private readonly IProductService _productService = productService;

    // GET: api/Category
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategory()
    {
        var categories = _productService.GetCategories();

        if (categories == null || !categories.Any())
        {
            return NoContent();
        }

        return Ok(categories);
    }

    // GET api/Category/5
    [HttpGet("{id}")]
    public ActionResult<Category> GetCategoryById(int id)
    {
        if (!IsValidId(id, out var errorResult))
            return errorResult;

        var category = _productService.GetCategory(id);

        if (category == null)
        {
            return NotFound();
        }

        return Ok(category);
    }

    // POST api/Category
    [HttpPost]
    public ActionResult<CategoryDto> PostCategory(CategoryDto categoryDTO)
    {
        var category = new Category
        {
            Name = categoryDTO.Name,
            Description = categoryDTO.Description,
        };

        _productService.AddCategory(category);

        return CreatedAtAction(nameof(GetCategoryById), new { id = category.CategoryId }, new
        {
            Id = category.CategoryId,
            category.Name,
            category.Description
        });
    }

    // PUT api/Category/5
    [HttpPut("{id}")]
    public IActionResult PutCategory(int id, CategoryDto categoryDTO)
    {
        if (!IsValidId(id, out var errorResult))
            return errorResult;

        var category = _productService.GetCategory(id);
        if (category == null)
        {
            return NotFound();
        }

        category.Name = categoryDTO.Name;
        category.Description = categoryDTO.Description;
        _productService.EditCategory(category);

        return NoContent();
    }

    // DELETE api/Category/5
    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        if (!IsValidId(id, out var errorResult))
            return errorResult;

        var category = _productService.GetCategory(id);
        if (category == null)
        {
            return NotFound();
        }

        _productService.DeleteCategory(id);

        return NoContent();
    }

    private bool IsValidId(int id, out ActionResult errorResult)
    {
        if (id <= 0)
        {
            errorResult = BadRequest("Id must be greater than 0.");
            return false;
        }

        errorResult = null!;
        return true;
    }
}
