namespace Domain.DTOs.TaskDTOs;

public class GetTaskDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}