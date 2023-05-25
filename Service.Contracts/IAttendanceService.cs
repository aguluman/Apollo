using Entities.LinkModels;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAttendanceService 
{
    //Todo : Add IAttendanceService
    Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeeAttendancesAsync(
        Guid employeeId, AttendanceLinkParameters attendanceLinkParameters, bool trackChanges);
    
    Task<AttendanceDto> GetEmployeeAttendanceAsync(Guid employeeId,
        Guid attendanceId, bool trackChanges);
    
    Task<AttendanceDto> CreateAttendanceForEmployeeAsync(Guid employeeId,
        AttendanceForCreationDto attendanceForCreation);
}