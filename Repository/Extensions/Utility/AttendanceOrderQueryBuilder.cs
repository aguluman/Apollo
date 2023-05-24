using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions.Utility
{
    public static class AttendanceOrderQueryBuilder
    {
        //Todo : Add AttendanceOrderQueryBuilder
        public static string CreateAttendanceOrderQuery<T>(string orderAttendanceByQueryString)
        {
            var orderAttendanceParams = orderAttendanceByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Attendance)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderAttendanceQueryBuilder = new StringBuilder();

            foreach (var param in orderAttendanceParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name
                    .Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderAttendanceQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            var orderAttendanceQuery = orderAttendanceQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderAttendanceQuery;
        }
    }
}
