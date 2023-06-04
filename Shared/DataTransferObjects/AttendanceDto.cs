
namespace Shared.DataTransferObjects;

public record AttendanceDto(Guid Id, DateTimeOffset ClockIn, DateTimeOffset? ClockOut,
    TimeSpan WorkHours, TimeSpan ActiveWorkTime, Guid EmployeeId, TimeSpan BreakTime,
    DateTimeOffset? BreakTimeStart, DateTimeOffset? BreakTimeEnd);