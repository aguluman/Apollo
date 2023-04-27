using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models.Enums;

namespace Entities.Models;

public class Tasks
{
    [Column("TaskId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title Of the task is required")]
    [MaxLength(90, ErrorMessage = "Maximum length for the Task is 90 characters")]
    public string Title { get; set; } = "";

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = "";
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    [Required(ErrorMessage = "Expected Date of Completion is required")]
    public DateTime DueAt { get; set; }
    
    [ForeignKey(nameof(Employee))]
    public Guid EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    [Required(ErrorMessage = "If no State is selected, default selection is NotStarted")]
    public State State { get; set; } = State.NotStarted;
    
    [Required(ErrorMessage = "If no Priority is selected, default is Normal")]
    public Priority Priority { get; set; } = Priority.Normal;
}


