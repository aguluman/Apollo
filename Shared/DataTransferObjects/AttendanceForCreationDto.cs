using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record AttendanceForCreationDto
{
    [Required(ErrorMessage = "Employee Id is required")]
    public Guid EmployeeId { get; init; }
    
    [Required(ErrorMessage = "Clock In is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockIn { get; init; }
    
    [Required(ErrorMessage = "Break Time is required")]
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = @"{0:hh\:mm}", ApplyFormatInEditMode = true)]
    [Range(typeof(TimeSpan), "0:00", "1:30")]
    public TimeSpan BreakTime { get; init; } 

}