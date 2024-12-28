using TarefasApi.Dtos.Tasks;

namespace TarefasApi.Dtos.Categories;
public class CategoryWithTasksDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<TaskDto> Tasks { get; set; } = new List<TaskDto>();
}