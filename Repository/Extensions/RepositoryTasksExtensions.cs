using Entities.Models;
using Repository.Extensions.Utility;

namespace Repository.Extensions;

public static class RepositoryTasksExtensions
{
    public static IQueryable<Tasks> FilterByDate(this IQueryable<Tasks> tasks, DateTime minTime, DateTime maxAge) =>
        tasks.Where(t => t.CreatedAt >= minTime && t.CreatedAt <= maxAge);

    public static IQueryable<Tasks> Search(this IQueryable<Tasks> tasks, string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return tasks;

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return tasks.Where(t => t.Title.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<Tasks> Sort(this IQueryable<Tasks> tasks, string orderByTaskQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByTaskQueryString))
            return tasks.OrderBy(t => t.Title);

        var orderQuery = TasksOrderQueryBuilder.CreateTasksOrderQuery<Tasks>(orderByTaskQueryString);

        return string.IsNullOrWhiteSpace(orderQuery)
            ? tasks.OrderBy(t => t.Title)
            : tasks.OrderBy(t => t.Title).ThenByDescending(o => o.State);
    }
}