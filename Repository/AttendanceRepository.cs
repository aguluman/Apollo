using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class AttendanceRepository : RepositoryBase<Attendance>, IAttendanceRepository
{
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

   

    public void SetClockInForAttendance(Guid employeeId, Attendance attendance)
    {
        attendance.EmployeeId = employeeId;
        Create(attendance);
    }

    
}