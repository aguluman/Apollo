using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAttendanceRepository
{
    Task<PagedList<Attendance>> GetEmployeeAttendancesAsync(Guid employeeId,
        AttendanceParameters attendanceParameters, bool trackChanges);
    
    
    Task<Attendance> GetEmployeeAttendanceAsync(Guid employeeId, 
        Guid attendanceId, bool trackChanges);

    void SetClockInForAttendance(Guid employeeId, Attendance attendance);

    
}