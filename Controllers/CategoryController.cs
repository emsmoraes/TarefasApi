using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Add(Category category)
    {
        var newCategory = await _service.AddAsync(category);
        return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Category category)
    {
        if(id != category.Id) return BadRequest();

        var existingCategory = await _service.GetByIdAsync(id);

        if(existingCategory is null) return NotFound();

        await _service.UpdateAsync(category);

        return NoContent();
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