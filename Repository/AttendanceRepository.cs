using System.Diagnostics;
using Contracts;
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

    public async Task<PagedList<Attendance>> GetEmployeesAttendancesByCompanyIdAsync(Guid companyId, 
        AttendanceParameters attendanceParameters, bool trackChanges)
    {
        Debug.Assert(attendanceParameters.SearchTerm != null, "attendanceParameters.SearchTerm != null");
        Debug.Assert(attendanceParameters.OrderBy != null, "attendanceParameters.OrderBy != null");
        var attendanceFromOneCompany = await FindByCondition(
                a => a.Employee.CompanyId.Equals(companyId), trackChanges)
            .FilterAttendances(attendanceParameters.StartDate, attendanceParameters.EndDate)
            .Search(attendanceParameters.SearchTerm)//search with position of employee
            .Sort(attendanceParameters.OrderBy)//order by clock-in time
            .Skip((attendanceParameters.PageNumber - 1) * attendanceParameters.PageSize)
            .Take(attendanceParameters.PageSize)
            .ToListAsync();
        
        return PagedList<Attendance>
            .ToPagedList(attendanceFromOneCompany,
                attendanceParameters.PageNumber, attendanceParameters.PageSize);
    }
    
    public async Task<PagedList<Attendance>> GetEmployeeAttendancesAsync(Guid employeeId,AttendanceParameters attendanceParameters, bool trackChanges)
    {
        Debug.Assert(attendanceParameters.SearchTerm != null, "attendanceParameters.SearchTerm != null");
        Debug.Assert(attendanceParameters.OrderBy != null, "attendanceParameters.OrderBy != null");
        var attendanceFromOnePosition = await FindByCondition(
                a => a.Employee.Id.Equals(employeeId), trackChanges)
            .FilterAttendances(attendanceParameters.StartDate, attendanceParameters.EndDate)
            .Sort(attendanceParameters.OrderBy)
            .Skip((attendanceParameters.PageNumber - 1) * attendanceParameters.PageSize)
            .Take(attendanceParameters.PageSize)
            .ToListAsync();
        
        return PagedList<Attendance>
            .ToPagedList(attendanceFromOnePosition,
                attendanceParameters.PageNumber, attendanceParameters.PageSize);
    }

    
    
    public async Task<Attendance> GetAttendanceAsync(Guid employeeId, Guid attendanceId, bool trackChanges) 
        => await FindByCondition(a => a.EmployeeId.Equals(employeeId) 
                                      && a.Id.Equals(attendanceId), trackChanges)
            .SingleOrDefaultAsync() ?? throw new InvalidOperationException();
    

    public void CreateAttendanceForEmployeesAsync(Guid employeeId, Attendance attendance)
    {
        attendance.EmployeeId = employeeId;
        Create(attendance);
    }
}