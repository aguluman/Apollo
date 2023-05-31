using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AttendanceForClockInDto
{
    [Required(ErrorMessage = "Employee Id is required")]
    public  Guid EmployeeId { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public  DateTimeOffset ClockIn { get; set; } = DateTimeOffset.Now;
}