using TarefasApi.Dtos;
using TarefasApi.Models;
using TarefasApi.Repositories;

namespace TarefasApi.Services;

public interface IUserService
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    System.Threading.Tasks.Task DeleteAsync(int id);
} 
public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User> AddAsync(User user)
    {
        return await _repository.AddAsync(user);
    }

    public async Task<User> UpdateAsync(User user)
    {
        return await _repository.UpdateAsync(user);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}