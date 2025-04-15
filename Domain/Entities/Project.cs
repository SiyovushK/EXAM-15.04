using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Tasks> Tasks { get; set; } = [];
}