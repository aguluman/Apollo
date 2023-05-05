using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Extensions.Utilities;

namespace Repository.Extensions;

public static class RepositoryAttendanceExtensions
{
    public static IQueryable<Attendance> FilterAttendances(this IQueryable<Attendance> attendances, DateTime startDate, DateTime endDate) =>
        attendances.Where(a => a.ClockIn >= startDate && a.ClockOut <= endDate);

    public static IQueryable<Attendance> Search(this IQueryable<Attendance> attendances, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return attendances;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return attendances.Where(a => a.Employee.Position != null && a.Employee.Position.ToLower().Contains(lowerCaseTerm));
    }
    
    public static IQueryable<Attendance> Sort(this IQueryable<Attendance> attendances, string orderByAttendanceQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByAttendanceQueryString))
            return attendances.OrderBy(a => a.ClockIn);

        var orderQuery = AttendanceOrderQueryBuilder.CreateAttendanceOrderQuery<Attendance>(orderByAttendanceQueryString);
        
        return string.IsNullOrWhiteSpace(orderQuery) ? attendances.OrderBy(a => a.ClockIn) 
            : attendances.OrderBy(orderQuery);
    }
}