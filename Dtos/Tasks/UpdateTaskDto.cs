namespace TarefasApi.Dtos.Tasks;
public class UpdateTaskDto
{
     public required string Name { get; set; }
        public required string Description { get; set; }
        public bool Concluded { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public required DateTime OccurrenceDate { get; set; }
}