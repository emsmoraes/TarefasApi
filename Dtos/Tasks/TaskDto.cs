using TarefasApi.Dtos.Categories;

namespace TarefasApi.Dtos.Tasks;
public class TaskDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Concluded { get; set; }
    public CategoryDto? Category { get; set; }
    public UserDto? User { get; set; }
}