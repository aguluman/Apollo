using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAttendanceRepository
{
    Task<PagedList<Attendance>> GetEmployeesAttendanceByCompanyId(Guid companyId,
        AttendanceParameters attendanceParameters, bool trackChanges);

    Task<PagedList<Attendance>> GetEmployeesAttendanceByPosition(string position, 
        AttendanceParameters attendanceParameters, bool trackChanges);
    
    Task<Attendance> GetAttendanceByEmployeeIdAndAttendanceId(Guid employeeId,
        Guid id, bool trackChanges);

    void CreateAttendanceForEmployees(Guid employeeId, Attendance attendance);
}