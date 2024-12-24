namespace TarefasApi.Models;
public class Task
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public bool Concluded { get; set; }
    public required Category Category { get; set; }
    public int CategoryId { get; set; }
}