namespace Domain.DTOs.TaskAssigmentDTOs;

public class UpdateTaskAssignmentDTO
{
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public DateTime AssignedDate { get; set; }
}