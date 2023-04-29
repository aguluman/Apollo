using Entities.LinkModels;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface ITaskService
{
    Task<(LinkResponse linkResponse ,MetaData metaData)> GetEmployeesTasksAsync(
        Guid employeeId, LinkParameters linkParameters, bool trackChanges);
    
    Task<TasksDto> GetEmployeeTaskAsync(Guid employeeId, Guid id, bool trackChanges);
    
    Task<TasksDto> CreateTaskForEmployeeAsync(
        Guid employeeId, TasksForCreationDto taskForCreation, bool trackChanges);
    
    Task DeleteTaskForEmployeeAsync(Guid employeeId, Guid id, bool trackChanges);
    
    Task UpdateTaskForEmployeeAsync(Guid employeeId, Guid id, TasksForUpdateDto taskForUpdate,
        bool taskTrackChanges);
    
    Task<(TasksForUpdateDto taskToPatch, Tasks taskEntity)> GetTaskForPatchAsync(
        Guid employeeId, Guid id, bool taskTrackChanges);
    
    Task SaveChangesForPatchAsync(TasksForUpdateDto taskToPatch, Tasks taskEntity);
}