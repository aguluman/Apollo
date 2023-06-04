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

    
}