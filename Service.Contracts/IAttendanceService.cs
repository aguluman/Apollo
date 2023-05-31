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
        AttendanceForClockInDto attendanceForClockIn);

    Task<AttendanceDto> CreateClockOutForAttendance(Guid employeeId, Guid id,
        AttendanceForClockOutDto attendanceForClockIn);

    Task<AttendanceDto> CreateBreakTimeClockIn(Guid employeeId, Guid id,
        AttendanceForBtClockInDto attendanceForClockIn);

    Task<AttendanceDto> CreateBreakTimeClockOut(Guid employeeId, Guid id,
        AttendanceForBtClockOutDto attendanceForClockIn);
    

}