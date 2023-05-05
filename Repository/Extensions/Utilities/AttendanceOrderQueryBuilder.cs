using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions.Utilities;

public static class AttendanceOrderQueryBuilder
{
    public static string CreateAttendanceOrderQuery<T>(string orderByAttendanceQueryString)
    {
        var attendanceOrderParams = orderByAttendanceQueryString.Trim().Split(',');
        var propertyInfos = typeof(Attendance)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var attendanceOrderQueryBuilder = new StringBuilder();

        foreach (var param in attendanceOrderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name
                .Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = param.EndsWith(" desc") ? "descending" : "ascending";
            attendanceOrderQueryBuilder.Append($"{objectProperty.Name} {direction},");
        }
        var attendanceOrderQuery = attendanceOrderQueryBuilder.ToString().TrimEnd(',', ' ');
        return attendanceOrderQuery;
    }
}