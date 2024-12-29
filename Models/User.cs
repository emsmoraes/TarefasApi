namespace TarefasApi.Models;
public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int Idade { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public List<Task> Task { get; set; } = new();
}