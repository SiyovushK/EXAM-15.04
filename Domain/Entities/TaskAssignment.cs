using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class TaskAssignment
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int TaskId { get; set; }
    [Required]
    public int UserId { get; set; }
    public DateTime AssignedDate { get; set; } = DateTime.Now;
    public Tasks Task { get; set; }
    public User User { get; set; }
}