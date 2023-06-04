using Entities.Models;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryAttendanceExtensions
{
    public static IQueryable<Attendance> FilterByClockIn(this IQueryable<Attendance> attendanceQuery,
        DateTimeOffset minClockIn, DateTimeOffset maxClockIn) =>
        attendanceQuery.Where(a => a.ClockIn >= minClockIn && a.ClockIn <= maxClockIn);
    
    public static IQueryable<Attendance> FilterByClockOut(this IQueryable<Attendance> attendanceQuery,
        DateTimeOffset minClockOut, DateTimeOffset maxClockOut) =>
        attendanceQuery.Where(a => a.ClockOut >= minClockOut && a.ClockOut <= maxClockOut);
    
    public static IQueryable<Attendance> FilterByEmployeeName(this IQueryable<Attendance> attendanceQuery,
        string employeeName)
    {
        return string.IsNullOrWhiteSpace(employeeName) 
            ? attendanceQuery 
            : attendanceQuery.Where(a => a.Employee.Name.Contains(employeeName.Trim()));
    }

    public static IQueryable<Attendance> FilterByCompanyName(this IQueryable<Attendance> attendanceQuery,
        string companyName)
    {
        return string.IsNullOrWhiteSpace(companyName) 
            ? attendanceQuery 
            : attendanceQuery.Where(a => a.Employee.Company.Name.Contains(companyName.Trim()));
    }

    public static IQueryable<Attendance> Sort(this IQueryable<Attendance> attendanceQuery,
        string orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
            return attendanceQuery.OrderBy(a => a.ActiveWorkTime);

        var orderQuery = AttendanceOrderQueryBuilder.CreateAttendanceOrderQuery<Attendance>(orderByQueryString);


        return string.IsNullOrWhiteSpace(orderQuery)
            ? attendanceQuery.OrderBy(a => a.ActiveWorkTime)
            : attendanceQuery.OrderBy(orderQuery);
    }
}