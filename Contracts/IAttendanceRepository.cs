using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAttendanceRepository
{
    Task<PagedList<Attendance>> GetEmployeesAttendancesByCompanyIdAsync(Guid companyId,
        AttendanceParameters attendanceParameters, bool trackChanges);

    Task<PagedList<Attendance>> GetEmployeeAttendancesAsync(Guid employeeId,
        AttendanceParameters attendanceParameters, bool trackChanges);
    
    Task<Attendance> GetAttendanceAsync(Guid employeeId,
        Guid attendanceId, bool trackChanges);

    void CreateAttendanceForEmployeesAsync(Guid employeeId, Attendance attendance);
}