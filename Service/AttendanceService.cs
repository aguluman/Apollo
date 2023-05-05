using System.Diagnostics;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class AttendanceService : IAttendanceService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IAttendanceLinks _attendanceLinks;
    
    public AttendanceService(IRepositoryManager repository, ILoggerManager logger,
        IMapper mapper, IAttendanceLinks attendanceLinks)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _attendanceLinks = attendanceLinks;
    }

    public async Task<(LinkResponse linkResponse, MetaData)> GetEmployeesAttendancesByCompanyIdAsync(
        Guid companyId, LinkParameters linkParameters, bool trackChanges)
    {
        Debug.Assert(linkParameters.AttendanceParameters != null, "linkParameters.AttendanceParameters != null");
        if(!linkParameters.AttendanceParameters.ValidDateRange)
            throw new MaxDateRangeBadRequestException();
        
        await CheckIfCompanyExists(companyId, trackChanges);
        
        var employeesAttendanceWithMetaData = await _repository.Attendance
            .GetEmployeesAttendancesByCompanyIdAsync(companyId, linkParameters.AttendanceParameters, trackChanges);
        
        var employeesAttendanceDto = _mapper.Map<IEnumerable<AttendanceDto>>(employeesAttendanceWithMetaData);
        
        var links = _attendanceLinks.TryGenerateLinks(employeesAttendanceDto, linkParameters.AttendanceParameters.Fields!,
            null, companyId, linkParameters.Context);

        return (linkResponse: links,  employeesAttendanceWithMetaData.MetaData);
    }

    public async Task<(LinkResponse linkResponse, MetaData)> GetEmployeeAttendancesAsync(
        Guid employeeId, LinkParameters linkParameters, bool trackChanges)
    {
        Debug.Assert(linkParameters.AttendanceParameters != null, "linkParameters.AttendanceParameters != null");
        if (!linkParameters.AttendanceParameters.ValidDateRange)
            throw new MaxDateRangeBadRequestException();
        
        var attendanceWithMetaData = await _repository.Attendance
            .GetEmployeeAttendancesAsync(employeeId, linkParameters.AttendanceParameters, trackChanges);
        
        var attendanceDto = _mapper.Map<IEnumerable<AttendanceDto>>(attendanceWithMetaData);

        var links = _attendanceLinks.TryGenerateLinks(attendanceDto, linkParameters.AttendanceParameters.Fields!,
            employeeId, null, linkParameters.Context);

        return (linkResponse: links,  attendanceWithMetaData.MetaData);
    }

    
    public async Task<AttendanceDto> GetAttendanceAsync(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendanceDb = await CheckIfAttendanceExists(employeeId, attendanceId, trackChanges);

        var attendance = _mapper.Map<AttendanceDto>(attendanceDb);
        return attendance;
    }

    
    public async Task<AttendanceDto> CreateAttendanceForEmployeesAsync(Guid employeeId, AttendanceForCreationDto attendanceForCreation,
        bool trackChanges)
    {
        var attendanceEntity = _mapper.Map<Attendance>(attendanceForCreation);
        
        _repository.Attendance.CreateAttendanceForEmployeesAsync(employeeId, attendanceEntity);
        await _repository.SaveAsync();

        var attendanceToReturn = _mapper.Map<AttendanceDto>(attendanceEntity);
        return attendanceToReturn;
    }
    
    
    
    
    private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company is null)
            throw new CompanyNotFoundException(companyId);
    }
    
    private async Task<Attendance> CheckIfAttendanceExists(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        var attendances = await _repository.Attendance.GetAttendanceAsync(employeeId, attendanceId, trackChanges);
        if (attendances is null)
            throw new AttendanceNotFoundException(attendanceId);

        return attendances;
    }
}