using TarefasApi.Dtos.Tasks;
using TarefasApi.Models;
using TarefasApi.Repositories;

namespace TarefasApi.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TarefasApi.Models.Task?> GetByIdAsync(int id);
    Task<TarefasApi.Models.Task> AddAsync(TarefasApi.Models.Task task);
    Task<TarefasApi.Models.Task> UpdateAsync(TarefasApi.Models.Task task);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository; 

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TarefasApi.Models.Task?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TarefasApi.Models.Task> AddAsync(TarefasApi.Models.Task task)
    {
        return await _repository.AddAsync(task);
    }

    public async Task<TarefasApi.Models.Task> UpdateAsync(TarefasApi.Models.Task task)
    {
        return await _repository.UpdateAsync(task);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}