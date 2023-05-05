using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AttendanceManipulationDto
{
    [Required(ErrorMessage = "Clock in is required")]
    public DateTime ClockIn { get; init; }

    [Required(ErrorMessage = "Clock out is required")]
    [Range(typeof(TimeSpan), "0:00", "23:59", ErrorMessage = "Clock out must be after clock in")]
    public DateTime ClockOut { get; init; }

    [Range(typeof(TimeSpan), "0:00", "1:30")]
    public TimeSpan? BreakTime { get; init; }

    [Required(ErrorMessage = "Employee Id is required")]
    public Guid EmployeeId { get; init; }
    
    public virtual void Validate()
    {
        if (ClockOut <= ClockIn)
            throw new ArgumentException("Clock out time must be after clock in time");

        if (!BreakTime.HasValue) return;
        var timeWorked = ClockOut - ClockIn;
        if (BreakTime > timeWorked)
        {
            throw new ArgumentException("Break time cannot be longer than time worked");
        }
    }
}