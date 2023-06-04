using System.Reflection;
using System.Text;
using Entities.Models;

namespace Repository.Extensions.Utility;

public static class TasksOrderQueryBuilder
{
    public static string CreateTasksOrderQuery<T>(string orderTasksByQueryString)
    {
        {
            var orderTasksParams = orderTasksByQueryString.Trim().Split(',');
            var propertyInfos = typeof(Tasks)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderTasksQueryBuilder = new StringBuilder();

            foreach (var param in orderTasksParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name
                    .Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
            
                if(objectProperty == null)
                    continue;

                var direction = param.EndsWith(" desc") ? "descending" : "ascending";
                orderTasksQueryBuilder.Append($"{objectProperty.Name} {direction},");
            }

            var orderTasksQuery = orderTasksQueryBuilder.ToString().TrimEnd(',', ' ');
            return orderTasksQuery;
        }
    }
}