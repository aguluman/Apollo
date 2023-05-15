using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class TasksRepository : RepositoryBase<Tasks>, ITasksRepository
{
    //TODO: Implement
    public TasksRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public async Task<PagedList<Tasks>> GetEmployeesTasksAsync
        (Guid employeeId, TasksParameters tasksParameters, bool trackChanges)
    {
        var tasks = await FindByCondition(t => t.EmployeeId.Equals(employeeId), trackChanges)
            .FilterByDate(tasksParameters.MinTime, tasksParameters.MaxTime)
            .Search(tasksParameters.SearchTerm)
            .Sort(tasksParameters.OrderBy)
            .Skip((tasksParameters.PageNumber - 1) * tasksParameters.PageSize)
            .Take(tasksParameters.PageSize)
            .ToListAsync();

        return PagedList<Tasks>
            .ToPagedList(tasks, tasksParameters.PageNumber, tasksParameters.PageSize);

    }

    public async Task<Tasks?> GetEmployeeTaskAsync
        (Guid employeeId, Guid id, bool trackChanges)
    {
        return await FindByCondition(t => 
                t.EmployeeId.Equals(employeeId) && t.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }

    public void CreateTaskForEmployee(Guid employeeId, Tasks task)
    {
        task.EmployeeId = employeeId;
        Create(task);
    }

    public void DeleteTask(Tasks task) => Delete(task);
}