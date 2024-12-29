namespace TarefasApi.Dtos.Users;
public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required int Idade { get; set; }
    public required IFormFile ProfilePicture { get; set; }

}