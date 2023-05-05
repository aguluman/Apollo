namespace Shared.DataTransferObjects;

public record AttendanceDto
{
    public Guid Id { get; init; }
    public DateTime ClockIn { get; init; }
    public DateTime? ClockOut { get; init; }
    public TimeSpan? BreakTime { get; init; }
    public Guid EmployeeId { get; init; }
    public Guid CompanyId { get; init; }
    
    public TimeSpan? TimeOffWork => ClockOut.HasValue
        ? (ClockOut.Value - ClockIn) - BreakTime
        : null;
}
