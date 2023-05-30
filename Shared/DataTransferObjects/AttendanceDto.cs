using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AttendanceDto(Guid Id, DateTimeOffset ClockIn, DateTimeOffset? ClockOut,
TimeSpan? TimeOffWork, TimeSpan? ActiveWorkTime, Guid EmployeeId, TimeSpan BreakTime, 
DateTimeOffset BreakTimeStart, DateTimeOffset BreakTimeEnd);