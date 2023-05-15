using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface ITasksRepository
{
    //TODO: Done
    Task<PagedList<Tasks>> GetEmployeesTasksAsync(Guid employeeId, TasksParameters tasksParameters, bool trackChanges);
    
    Task<Tasks?> GetEmployeeTaskAsync(Guid employeeId, Guid id, bool trackChanges);
    
    void CreateTaskForEmployee(Guid employeeId, Tasks task);
    
    void DeleteTask(Tasks task);
}