using Microsoft.AspNetCore.Mvc;
using TarefasApi.Dtos;
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
    private readonly IUserService _userService;

    public TaskController(ITaskService taskService, ICategoryService categoryService, IUserService userService)
    {
        _taskService = taskService;
        _categoryService = categoryService;
        _userService = userService;
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
        var user = await _userService.GetByIdAsync(createTaskDto.UserId);

        if (category is null || user is null)
            return NotFound();

        var task = new TarefasApi.Models.Task()
        {
            Name = createTaskDto.Name,
            Description = createTaskDto.Description,
            OccurrenceDate = createTaskDto.OccurrenceDate,
            Category = category,
            User = user
        };

        var createdTask = await _taskService.AddAsync(task);
        var categoryDto = new CategoryDto()
        {
            Id = category.Id,
            Name = category.Name
        };
        var userDto = new UserDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Idade = user.Idade
        };
        var taskDto = new TaskDto()
        {
            Id = createdTask.Id,
            Name = createdTask.Name,
            Description = createdTask.Description,
            Concluded = createdTask.Concluded,
            OccurrenceDate = createdTask.OccurrenceDate,
            Category = categoryDto,
            User = userDto
        };
        return CreatedAtAction(nameof(GetById), new { id = taskDto.Id }, taskDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        var category = await _categoryService.GetByIdAsync(updateTaskDto.CategoryId);
        var user = await _userService.GetByIdAsync(updateTaskDto.UserId);

        if (category is null || user is null)
            return NotFound();

        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();

        task.Name = updateTaskDto.Name;
        task.Description = updateTaskDto.Description;
        task.Concluded = updateTaskDto.Concluded;
        task.Category = category;
        task.OccurrenceDate = updateTaskDto.OccurrenceDate;
        task.User = user;

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