using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AttendanceForBtClockOutDto 
{
    [Required(ErrorMessage = "Employee Id is required")]
    public Guid EmployeeId { get; set; }
    
    [Required(ErrorMessage = "Attendance Id is required")]
    public Guid AttendanceId { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset BreakTimeEnd { get; set; } = DateTimeOffset.Now;
}