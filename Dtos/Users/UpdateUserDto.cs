namespace TarefasApi.Dtos;
public class UpdateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int Idade { get; set; }
}