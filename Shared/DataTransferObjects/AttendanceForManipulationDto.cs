using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record AttendanceForManipulationDto
{
    [Required(ErrorMessage = "Employee Id is required")]
    public Guid EmployeeId { get; init; }
        
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockIn { get; init; } = DateTimeOffset.Now;

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset ClockOut { get; init; } = DateTimeOffset.Now;
        
    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset BreakTimeStart { get; init; } = DateTimeOffset.Now;

    [DataType(DataType.Time)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
    public DateTimeOffset BreakTimeEnd { get; init; } = DateTimeOffset.Now;
}