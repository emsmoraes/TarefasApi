using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Dtos.Tasks;
using TarefasApi.Dtos.Categories;
using TarefasApi.Models;

namespace TarefasApi.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryWithTasksDto>> GetAllAsync();
    Task<Category> GetByIdAsync(int id);
    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
public class CategoryRepository : ICategoryRepository
{
    private readonly TasksContext _context;

    public CategoryRepository(TasksContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryWithTasksDto>> GetAllAsync()
    {
        var categories = await _context.Categories.Include(c => c.Tasks).ToListAsync();
        return categories.Select(c => new CategoryWithTasksDto
        {
            Id = c.Id,
            Name = c.Name,
            Tasks = c.Tasks.Select(t => new TaskDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description,
                Concluded = t.Concluded
            }).ToList()
        });
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category> AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);

        if (category is not null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}