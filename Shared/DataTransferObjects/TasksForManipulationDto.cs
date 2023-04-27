using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record TasksForManipulationDto
{
    [Required(ErrorMessage = "Title Of the task is required")]
    [MaxLength(90, ErrorMessage = "Maximum length for the Task is 90 characters")]
    public string Title { get; init; } = "";

    [Required(ErrorMessage = "Description is required")]
    public string Description { get; init; } = "";
    
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    
    [Required(ErrorMessage = "Expected Date of Completion is required")]
    public DateTime DueAt { get; init; }
}