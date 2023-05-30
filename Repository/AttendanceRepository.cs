using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class AttendanceRepository : RepositoryBase<Attendance>, IAttendanceRepository
{
    //Todo : Add AttendanceRepository
    public AttendanceRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Attendance>> GetEmployeeAttendancesAsync(Guid employeeId, AttendanceParameters attendanceParameters, bool trackChanges)
    {
        var attendances = await FindByCondition(a => a.EmployeeId.Equals(employeeId), trackChanges)
            .FilterByClockIn(attendanceParameters.MinClockIn, attendanceParameters.MaxClockIn)
            .FilterByClockOut(attendanceParameters.MinClockOut, attendanceParameters.MaxClockOut)
            .FilterByEmployeeName(attendanceParameters.EmployeeName)
            .FilterByCompanyName(attendanceParameters.CompanyName)
            .Sort(attendanceParameters.OrderBy ?? throw new SortOrderByExceptionHandler())
            .Skip((attendanceParameters.PageNumber - 1) * attendanceParameters.PageSize)
            .Take(attendanceParameters.PageSize)
            .ToListAsync();
        
        return PagedList<Attendance>
            .ToPagedList(attendances,
                attendanceParameters.PageNumber, attendanceParameters.PageSize);
    }

    public async Task<Attendance> GetEmployeeAttendanceAsync(Guid employeeId, Guid attendanceId, bool trackChanges)
    {
        return await FindByCondition(a =>
                a.EmployeeId.Equals(employeeId) && a.Id.Equals(attendanceId), trackChanges)
            .SingleOrDefaultAsync() ?? throw new EmployeeAttendanceNotFoundException();
    }

   

    public void SetClockInForAttendance(Guid employeeId, Attendance attendance, DateTimeOffset clockIn)
    {
        attendance.ClockIn = clockIn;
        attendance.EmployeeId = employeeId;
        Create(attendance);
    }

    public void SetClockOutForAttendance(Guid employeeId, Attendance attendance, DateTimeOffset clockOut)
    {
        attendance.ClockOut = clockOut;
        attendance.EmployeeId = employeeId;
        Update(attendance);
        
        // Calculate and set the TimeOffWork value
        attendance.TimeOffWork = CalculateTimeOffWork(attendance);
        
        // Calculate and set the ActiveWorkTime value
        attendance.ActiveWorkTime = CalculateActiveWorkTime(attendance);
        Update(attendance);
    }

    public void SetBreakTimeClockIn(Guid employeeId, Attendance attendance, DateTimeOffset btClockIn)
    {
        attendance.BreakTimeStart = btClockIn;
        attendance.EmployeeId = employeeId;
        Update(attendance);
    }

    public void SetBreakTimeClockOut(Guid employeeId, Attendance attendance, DateTimeOffset btClockOut)
    {
        attendance.BreakTimeEnd = btClockOut;
        attendance.EmployeeId = employeeId;
        Update(attendance);
        
        // Calculate and set the BreakTime value
        attendance.BreakTime = CalculateBreakTime(attendance);
        Update(attendance);
    }

    public TimeSpan CalculateTimeOffWork(Attendance attendance)
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
}