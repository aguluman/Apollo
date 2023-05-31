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

    void SetClockInForAttendance(Guid employeeId, Attendance attendance);

    void SetClockOutForAttendance(Guid employeeId, Guid attendanceId, Attendance attendance);

    void SetBreakTimeClockIn(Guid employeeId, Guid attendanceId, Attendance attendance);

    void SetBreakTimeClockOut(Guid employeeId, Guid attendanceId, Attendance attendance);
    
    TimeSpan CalculateTimeOffWork(Attendance attendance);
    
    TimeSpan CalculateBreakTime(Attendance attendance);
    
    TimeSpan CalculateActiveWorkTime(Attendance attendance);
}