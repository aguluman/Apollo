namespace Shared.DataTransferObjects;

public record AttendanceDto(
    Guid Id,
    DateTime ClockIn,
    DateTime? ClockOut,
    TimeSpan? BreakTime, 
    Guid EmployeeId,
    Guid CompanyId)
{
    public TimeSpan? TimeOffWork => ClockOut.HasValue
        ? (ClockOut.Value - ClockIn) - BreakTime
        : null;
}