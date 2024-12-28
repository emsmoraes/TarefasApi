using Microsoft.AspNetCore.Mvc;
using TarefasApi.Dtos.Categories;
using TarefasApi.Dtos.Tasks;
using TarefasApi.Services;

namespace TarefasApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly ICategoryService _categoryService;

    public TaskController(ITaskService taskService, ICategoryService categoryService)
    {
        _taskService = taskService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTaskDto createTaskDto)
    {
        var category = await _categoryService.GetByIdAsync(createTaskDto.CategoryId);
        Console.WriteLine(category);
        if (category is null) return NotFound();
        var task = new TarefasApi.Models.Task()
        {
            Name = createTaskDto.Name,
            Description = createTaskDto.Description,
            Category = category
        };

        var createdTask = await _taskService.AddAsync(task);
        var categoryDto = new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name
        };
        var taskDto = new TaskDto()
        {
            Id = createdTask.Id,
            Name = createdTask.Name,
            Description = createdTask.Description,
            Concluded = createdTask.Concluded,
            Category = categoryDto
        };
        return CreatedAtAction(nameof(GetById), new { id = taskDto.Id }, taskDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var category = await _categoryService.GetByIdAsync(updateTaskDto.CategoryId);
        if (category is null) return NotFound();
        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();

        task.Name = updateTaskDto.Name;
        task.Description = updateTaskDto.Description;
        task.Concluded = updateTaskDto.Concluded;
        task.Category = category;

        await _taskService.UpdateAsync(task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();

        await _taskService.DeleteAsync(id);
        return NoContent();
    }
}