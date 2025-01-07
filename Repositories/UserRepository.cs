using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Dtos;
using TarefasApi.Models;

namespace TarefasApi.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
public class UserRepository : IUserRepository
{
    private readonly TasksContext _context;

    public UserRepository(TasksContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var baseUrl = "https://localhost:5000";

        var users = await _context.Users.ToListAsync();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            Idade = u.Idade,
            ProfilePictureUrl = $"{baseUrl}/{u.ProfilePictureUrl}"
        });
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
        return user;
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var user = _context.Users.Find(id);
        _context.Users.Remove(user);
        _context.SaveChanges();
    }
}