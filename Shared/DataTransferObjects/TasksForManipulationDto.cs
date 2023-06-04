using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record TasksForManipulationDto
{
    [Required(ErrorMessage = "Title Of the task is required")]
    [MaxLength(90, ErrorMessage = "Maximum length for the Task is 90 characters")]
    public string Title { get; init; } = "";

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; init; } = "";
    
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.Now;
    
    [Required(ErrorMessage = "Expected Date of Completion is required")]
    public DateTimeOffset DueAt { get; init; }
    
    [Required(ErrorMessage = "If no State is selected, default selection is NotStarted")]
    public string State { get; set; }
    
    [Required(ErrorMessage = "If no Priority is selected, default is Normal")]
    public string Priority { get; set; }
}