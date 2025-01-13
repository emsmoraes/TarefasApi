using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Dtos;
using TarefasApi.Dtos.Categories;
using TarefasApi.Dtos.Tasks;

namespace TarefasApi.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TarefasApi.Models.Task> GetByIdAsync(int id);
    Task<TarefasApi.Models.Task> AddAsync(TarefasApi.Models.Task task);
    Task<TarefasApi.Models.Task> UpdateAsync(TarefasApi.Models.Task task);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
public class TaskRepository : ITaskRepository
{
    private readonly TasksContext _context;

    public TaskRepository(TasksContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        var tasks = await _context.Tasks
      .Include(t => t.Category)
      .Include(t => t.User)
      .ToListAsync();
        return tasks.Select(t => new TaskDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description,
            Concluded = t.Concluded,
            OccurrenceDate = t.OccurrenceDate,
            Category = new CategoryDto()
            {
                Id = t.Category.Id,
                Name = t.Category.Name,
            },
            User = new Dtos.UserDto()
            {
                Id = t.User.Id,
                Name = t.User.Name,
                Email = t.User.Email,
                Idade = t.User.Idade
            }
        });
    }

    public async Task<TarefasApi.Models.Task?> GetByIdAsync(int id)
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);


        if (task is null)
            return null;

        return task;
    }

    public async Task<TarefasApi.Models.Task> AddAsync(TarefasApi.Models.Task task)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TarefasApi.Models.Task> UpdateAsync(TarefasApi.Models.Task task)
    {
        _context.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is not null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }
}