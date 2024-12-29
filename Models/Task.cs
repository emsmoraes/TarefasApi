namespace TarefasApi.Models;
public class Task
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public bool Concluded { get; set; }
    public Category Category { get; set; }
    public User User { get; set; }
}