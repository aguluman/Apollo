using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAttendanceRepository
{
    //Todo : Add IAttendanceRepository, more to be done.
    Task<PagedList<Attendance>> GetEmployeeAttendancesAsync(Guid employeeId,
        AttendanceParameters attendanceParameters, bool trackChanges);
    
    
    Task<Attendance> GetEmployeeAttendanceAsync(Guid employeeId, 
        Guid attendanceId, bool trackChanges);

    void SetClockInForAttendance(Guid employeeId, Attendance attendance, DateTimeOffset clockIn);

    void SetClockOutForAttendance(Guid employeeId, Attendance attendance, DateTimeOffset clockOut);

    void SetBreakTimeClockIn(Guid employeeId, Attendance attendance, DateTimeOffset btClockIn);

    void SetBreakTimeClockOut(Guid employeeId, Attendance attendance, DateTimeOffset btClockOut);
    
    TimeSpan CalculateTimeOffWork(Attendance attendance);
    
    TimeSpan CalculateBreakTime(Attendance attendance);
    
    TimeSpan CalculateActiveWorkTime(Attendance attendance);
}