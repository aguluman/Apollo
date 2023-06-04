using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class TasksService : ITasksService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly ITasksLinks _tasksLinks;
    
    public TasksService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, ITasksLinks tasksLinks)
    {
        _repositoryManager = repositoryManager;
        _loggerManager = loggerManager;
        _mapper = mapper;
        _tasksLinks = tasksLinks;
    }


    public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeeTasksAsync(
        Guid employeeId, TasksLinkParameters tasksLinkParameters, bool trackChanges)
    {
        //This gets the lists of tasks for a specific employee
        if(!tasksLinkParameters.TasksParameters.ValidTimeRange)
            throw new MaxTimeRangeBadRequestException();
        
        var tasksWithMetaData = await _repositoryManager.Tasks
            .GetEmployeesTasksAsync(employeeId, tasksLinkParameters.TasksParameters, trackChanges);
        
        var tasksDto = _mapper.Map<IEnumerable<TasksDto>>(tasksWithMetaData);
        
        var links = _tasksLinks.TryGenerateLinks(tasksDto,
            tasksLinkParameters.TasksParameters.Fields, employeeId, tasksLinkParameters.Context);
        
        return (linkResponse: links, metaData: tasksWithMetaData.MetaData);
    }

    public async Task<TasksDto> GetEmployeeTaskAsync(Guid employeeId, Guid id, bool trackChanges)
    {
        //This gets a specific task for a specific employee
        var taskDb = await GetTasksByEmployeeAndCheckIfItExists(employeeId, id, trackChanges);
        
        var task = _mapper.Map<TasksDto>(taskDb);
        return task;
    }

    public async Task<TasksDto> CreateTaskForEmployeeAsync(Guid employeeId, TasksForCreationDto taskForCreation)
    {
        var taskEntity = _mapper.Map<Tasks>(taskForCreation);
        _repositoryManager.Tasks.CreateTaskForEmployee(employeeId, taskEntity);
        await _repositoryManager.SaveAsync();
        
        var taskToReturn = _mapper.Map<TasksDto>(taskEntity);
        return taskToReturn;
    }

    public async Task DeleteTaskForEmployeeAsync(Guid employeeId, Guid id, bool trackChanges)
    {
        var taskDb = await GetTasksByEmployeeAndCheckIfItExists(employeeId, id, trackChanges);
        _repositoryManager.Tasks.DeleteTask(taskDb);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateTaskForEmployeeAsync(Guid employeeId, Guid id, TasksForUpdateDto taskForUpdate, bool taskTrackChanges)
    {
        var taskDb = await GetTasksByEmployeeAndCheckIfItExists(employeeId, id, taskTrackChanges);
        _mapper.Map(taskForUpdate, taskDb);
        await _repositoryManager.SaveAsync();
    }

    public async Task<(TasksForUpdateDto taskToPatch, Tasks taskEntity)> GetTaskForPatchAsync(Guid employeeId, Guid id, bool taskTrackChanges)
    {
        var taskDb = await GetTasksByEmployeeAndCheckIfItExists(employeeId, id, taskTrackChanges);
        var taskToPatch = _mapper.Map<TasksForUpdateDto>(taskDb);
        return (taskToPatch, taskDb);
    }

    public async Task SaveChangesForPatchAsync(TasksForUpdateDto taskToPatch, Tasks taskEntity)
    {
        _mapper.Map(taskToPatch, taskEntity);
        await _repositoryManager.SaveAsync();
    }

    private async Task<Tasks> GetTasksByEmployeeAndCheckIfItExists(Guid employeeId, Guid taskId, bool trackChanges)
    {
        var tasks = await _repositoryManager.Tasks.GetEmployeeTaskAsync(employeeId, taskId, trackChanges);
        if (tasks is null) 
            throw new TasksNotFoundException(taskId);

        return tasks;
    }
}