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

    public AttendanceService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IAttendanceLinks attendanceLinks)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
        _attendanceLinks = attendanceLinks;
    }
    
    public async Task<(LinkResponse linkResponse, MetaData metaData)> 
        GetEmployeeAttendancesAsync(Guid employeeId, AttendanceLinkParameters attendanceLinkParameters, bool trackChanges)
    {
        if(!attendanceLinkParameters.AttendanceParameters.ValidClockInRange || !attendanceLinkParameters.AttendanceParameters.ValidClockOutRange)
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
        var attendanceDb = await GetAttendanceFormEmployeeAndCheckIfItExists(employeeId, attendanceId, trackChanges);

        var attendance = _mapper.Map<AttendanceDto>(attendanceDb);
        return attendance;
    }

    public async Task<AttendanceDto> CreateAttendanceForEmployeeAsync(
        Guid employeeId,
        AttendanceForCreationDto attendanceForCreation)
    {
        var attendanceEntity = _mapper.Map<Attendance>(attendanceForCreation);
        
        _repositoryManager.Attendance.CreateAttendanceForEmployee(employeeId, attendanceEntity);
        await _repositoryManager.SaveAsync();

        var attendanceToReturn = _mapper.Map<AttendanceDto>(attendanceEntity);
        return attendanceToReturn;
    }
    
    private async Task<Attendance> GetAttendanceFormEmployeeAndCheckIfItExists(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendance = await _repositoryManager.Attendance.GetEmployeeAttendanceAsync(employeeId, attendanceId, trackChanges);
        if (attendance is null) 
            throw new AttendanceNotFoundException(attendanceId);

        return attendance;
    }
}