using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAttendanceRepository
{
    //Todo : Add IAttendanceRepository
    Task<PagedList<Attendance>> GetAttendancesAsync(Guid employeeId,
        AttendanceParameters attendanceParameters, bool trackChanges);
    
    
    Task<Attendance> GetEmployeeAttendanceAsync(Guid employeeId, 
        Guid attendanceId, bool trackChanges);

    void CreateAttendance(Guid employeeId, Attendance attendance);
}