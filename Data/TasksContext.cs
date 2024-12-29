using Microsoft.EntityFrameworkCore;
using TarefasApi.Models;

namespace TarefasApi.Data;
public class TasksContext : DbContext
{
    public TasksContext(DbContextOptions<TasksContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<TarefasApi.Models.Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}