using Entities.LinkModels;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAttendanceService
{
    Task<(LinkResponse linkResponse, MetaData)> GetEmployeesAttendancesByCompanyIdAsync(
        Guid companyId, LinkParameters linkParameters, bool trackChanges);
    
    Task<(LinkResponse linkResponse, MetaData)> GetEmployeeAttendancesAsync(
        Guid employeeId, LinkParameters linkParameters, bool trackChanges);
    
    Task<AttendanceDto> GetAttendanceAsync(
        Guid employeeId, Guid attendanceId, bool trackChanges);
    
    Task<AttendanceDto> CreateAttendanceForEmployeesAsync(
        Guid employeeId, AttendanceForCreationDto attendanceForCreation, bool trackChanges);
}