using Microsoft.AspNetCore.Mvc;
using TarefasApi.Dtos.Categories;
using TarefasApi.Models;
using TarefasApi.Services;

namespace TarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category is null) return NotFound();
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateCategoryDto createCategoryDto)
    {
        var category = new Category(){
            Name = createCategoryDto.Name
        };
        
        var createdCategory = await _service.AddAsync(category);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto UpdateCategoryDto)
    {
      var existingCategory = await _service.GetByIdAsync(id);

      if(existingCategory is null) return NotFound();

      existingCategory.Name = UpdateCategoryDto.Name;

      return Ok(existingCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var existingCategory = await _service.GetByIdAsync(id);

        if(existingCategory is null) return NotFound();

        await _service.DeleteAsync(id);

        return NoContent();
    }
}