using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Tasks
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required, MaxLength(100)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public int ProjectId { get; set; }
    [Required]
    public int UserId { get; set; }
    public Project Project { get; set; }
    public User User { get; set; }
    public List<TaskAssignment> TaskAssignments { get; set; } = [];
}