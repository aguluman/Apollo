using Entities.LinkModels;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAttendanceService 
{
    //Todo : Add IAttendanceService, more to be done.
    Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeeAttendancesAsync(
        Guid employeeId, AttendanceLinkParameters attendanceLinkParameters, bool trackChanges);
    
    Task<AttendanceDto> GetEmployeeAttendanceAsync(Guid employeeId,
        Guid attendanceId, bool trackChanges);

    Task<AttendanceDto> CreateClockInForAttendance(Guid employeeId,
        AttendanceForCreationDto attendanceForCreation, DateTimeOffset clockIn);

    Task<AttendanceDto> CreateClockOutForAttendance(Guid employeeId,
        AttendanceForCreationDto attendanceForCreation, DateTimeOffset clockOut);

    Task<AttendanceDto> CreateBreakTimeClockIn(Guid employeeId, 
        AttendanceForCreationDto attendanceForCreation, DateTimeOffset btClockIn);

    Task<AttendanceDto> CreateBreakTimeClockOut(Guid employeeId,
        AttendanceForCreationDto attendanceForCreation, DateTimeOffset btClockOut);
    

}