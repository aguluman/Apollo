using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class AttendanceService : IAttendanceService
{
    //Todo : Add AttendanceService
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly IAttendanceLinks _attendanceLinks;

    public AttendanceService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper,
        IAttendanceLinks attendanceLinks)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
        _attendanceLinks = attendanceLinks;
    }

    public async Task<(LinkResponse linkResponse, MetaData metaData)>
        GetEmployeeAttendancesAsync(Guid employeeId, AttendanceLinkParameters attendanceLinkParameters,
            bool trackChanges)
    {
        if (!attendanceLinkParameters.AttendanceParameters.ValidClockInRange ||
            !attendanceLinkParameters.AttendanceParameters.ValidClockOutRange)
            throw new MaxClockInOrOutRangeBadRequestException();

        var attendanceWithMetaData = await _repositoryManager.Attendance
            .GetEmployeeAttendancesAsync(employeeId, attendanceLinkParameters.AttendanceParameters, trackChanges);

        var attendancesDto = _mapper.Map<IEnumerable<AttendanceDto>>(attendanceWithMetaData);

        var links = _attendanceLinks.TryGenerateLinks(attendancesDto,
            attendanceLinkParameters.AttendanceParameters.Fields, employeeId, attendanceLinkParameters.Context);

        return (linkResponse: links, metaData: attendanceWithMetaData.MetaData);
    }

    public async Task<AttendanceDto> GetEmployeeAttendanceAsync(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendanceDb = await GetAttendanceFromEmployeeAndCheckIfItExists(employeeId, attendanceId, trackChanges);

        var attendance = _mapper.Map<AttendanceDto>(attendanceDb);
        return attendance;
    }

    public async Task<AttendanceDto> CreateClockInForAttendance(Guid employeeId,
        AttendanceForCreationDto attendanceForClockInCreation)
    {
        var attendanceEntity = _mapper.Map<Attendance>(attendanceForClockInCreation);

        _repositoryManager.Attendance.SetClockInForAttendance(employeeId, attendanceEntity);
        await _repositoryManager.SaveAsync();

        var attendanceToReturn = _mapper.Map<AttendanceDto>(attendanceEntity);
        return attendanceToReturn;
    }

    public async Task<(AttendanceForUpdateDto attendanceDataToPatch, Attendance attendanceEntity)>
        SetClockOutForAttendance(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendanceDb = await GetAttendanceFromEmployeeAndCheckIfItExists(employeeId, attendanceId, trackChanges);
        var attendanceClockOutDataToPatch = _mapper.Map<AttendanceForUpdateDto>(attendanceDb);
        return (attendanceClockOutDataToPatch, attendanceDb);
    }

    public async Task<(AttendanceForUpdateDto attendanceDataToPatch, Attendance attendanceEntity)>
        SetBreakTimeClockIn(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendanceDb = await GetAttendanceFromEmployeeAndCheckIfItExists(employeeId, attendanceId, trackChanges);
        var attendanceBtClockInDataToPatch = _mapper.Map<AttendanceForUpdateDto>(attendanceDb);
        return (attendanceBtClockInDataToPatch, attendanceDb);
    }

    public async Task<(AttendanceForUpdateDto attendanceDataToPatch, Attendance attendanceEntity)>
        SetBreakTimeClockOut(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendanceDb = await GetAttendanceFromEmployeeAndCheckIfItExists(employeeId, attendanceId, trackChanges);
        var attendanceBtClockOutDataToPatch = _mapper.Map<AttendanceForUpdateDto>(attendanceDb);
        return (attendanceBtClockOutDataToPatch, attendanceDb);
    }

    public async Task SaveChangesForPatchAsync(AttendanceForUpdateDto attendanceDataToPatch,
        Attendance attendanceEntity)
    {
        _mapper.Map(attendanceDataToPatch, attendanceEntity);
        
        await _repositoryManager.SaveAsync();
    }

    //Todo: It worked.
    public async Task SaveChangesForCalculationsAsync(Attendance attendanceEntity)
    {
        // Set the BreakTime value
        attendanceEntity.BreakTime = CalculateBreakTime(attendanceEntity);
        
        // Set the TimeOffWork value
        attendanceEntity.TimeAtWork = CalculateTimeAtWork(attendanceEntity);
        
        // Set the ActiveWorkTime value
        attendanceEntity.ActiveWorkTime = CalculateActiveWorkTime(attendanceEntity);
        
        await _repositoryManager.SaveAsync();    
    }

    public TimeSpan CalculateTimeAtWork(Attendance attendance)
    {
        // Calculate and return the TimeOffWork value based on the attendance properties
        return attendance.ClockOut - attendance.ClockIn;
    }

    public TimeSpan CalculateBreakTime(Attendance attendance)
    {
        // Calculate and return the BreakTime value based on the attendance properties
        return attendance.BreakTimeEnd - attendance.BreakTimeStart;
    }

    public TimeSpan CalculateActiveWorkTime(Attendance attendance)
    {
        // Calculate and return the ActiveWorkTime value based on the attendance properties
        return attendance.ClockOut - attendance.ClockIn - CalculateBreakTime(attendance);
    }
    //Todo: Check the top for more info

    private async Task<Attendance> GetAttendanceFromEmployeeAndCheckIfItExists(Guid employeeId, Guid attendanceId,
        bool trackChanges)
    {
        var attendance =
            await _repositoryManager.Attendance.GetEmployeeAttendanceAsync(employeeId, attendanceId, trackChanges);
        if (attendance is null)
            throw new AttendanceNotFoundException(attendanceId);

        return attendance;
    }
}