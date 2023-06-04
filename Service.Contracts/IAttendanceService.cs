using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IAttendanceService 
{
    Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeeAttendancesAsync(
        Guid employeeId, AttendanceLinkParameters attendanceLinkParameters, bool trackChanges);
    
    Task<AttendanceDto> GetEmployeeAttendanceAsync(Guid employeeId,
        Guid attendanceId, bool trackChanges);

    Task<AttendanceDto> CreateClockInForAttendance(Guid employeeId,
        AttendanceForCreationDto attendanceForClockIn);

    Task<(AttendanceForUpdateDto attendanceDataToPatch, Attendance attendanceEntity)>
        SetClockOutForAttendance(Guid employeeId, Guid attendanceId,
            bool trackChanges);
    
    Task<(AttendanceForUpdateDto attendanceDataToPatch, Attendance attendanceEntity)>
        SetBreakTimeClockIn(Guid employeeId, Guid attendanceId,
        bool trackChanges);

    Task<(AttendanceForUpdateDto attendanceDataToPatch, Attendance attendanceEntity)>
        SetBreakTimeClockOut(Guid employeeId, Guid attendanceId,
            bool trackChanges);
    
    Task SaveChangesForPatchAsync(AttendanceForUpdateDto attendanceDataToPatch,
        Attendance attendanceEntity);

    Task SaveChangesForCalculationsAsync(Attendance attendanceEntity);
    
    TimeSpan CalculateTimeAtWork(Attendance attendance);
    
    TimeSpan CalculateBreakTime(Attendance attendance);
    
    TimeSpan CalculateActiveWorkTime(Attendance attendance);

}