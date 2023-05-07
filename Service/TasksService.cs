using System.Diagnostics;
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.LinkModels;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class TasksService : ITaskService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;
    private readonly ITaskLinks _taskLinks;

    public TasksService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, ITaskLinks taskLinks)
    {
        _repositoryManager = repository;
        _loggerManager = logger;
        _mapper = mapper;
        _taskLinks = taskLinks;
    }
    
    public async Task<(LinkResponse linkResponse, MetaData metaData)> GetEmployeesTasksAsync
        (Guid employeeId, TasksLinkParameters linkParameters, bool trackChanges)
    {
        Debug.Assert(linkParameters.TasksParameters != null, "linkParameters.TasksParameters != null");
        if (!linkParameters.TasksParameters.ValidTimeRange)
            throw new MaxTimeRangeBadRequestException();

        var tasksWithMetaData = await _repositoryManager.Tasks
            .GetEmployeesTasksAsync(employeeId, linkParameters.TasksParameters, trackChanges);
        
        var taskDto = _mapper.Map<IEnumerable<TasksDto>>(tasksWithMetaData);
        
        var links = _taskLinks.TryGenerateLinks(taskDto, linkParameters.TasksParameters.Fields!, employeeId,
            linkParameters.Context);

        return (linkResponse: links, metaData: tasksWithMetaData.MetaData);
    }

    public async Task<TasksDto> GetEmployeeTaskAsync(Guid employeeId, Guid id, bool trackChanges)
    {
        var taskDb = await GetTasksFromEmployeesAndCheckIfItExists(employeeId, id, trackChanges);

        var task = _mapper.Map<TasksDto>(taskDb);
        return task;
    }

    public async Task<TasksDto> CreateTaskForEmployeeAsync(Guid employeeId, 
        TasksForCreationDto taskForCreation, bool trackChanges)
    {
        var taskEntity = _mapper.Map<Tasks>(taskForCreation);
        _repositoryManager.Tasks.CreateTaskForEmployee(employeeId, taskEntity);
        await _repositoryManager.SaveAsync();
        
        var taskToReturn = _mapper.Map<TasksDto>(taskEntity);
        return taskToReturn;
    }

    public async Task DeleteTaskForEmployeeAsync(Guid employeeId, Guid id, bool trackChanges)
    {
        var taskForEmployeeDb = await GetTasksFromEmployeesAndCheckIfItExists(employeeId, id, trackChanges);
        _repositoryManager.Tasks.DeleteTask(taskForEmployeeDb);
        await _repositoryManager.SaveAsync();
    }

    public async Task UpdateTaskForEmployeeAsync(Guid employeeId, Guid id, 
        TasksForUpdateDto taskForUpdate, bool taskTrackChanges)
    {
        var taskForEmployeeDb = await GetTasksFromEmployeesAndCheckIfItExists(employeeId, id, taskTrackChanges);
        _mapper.Map(taskForUpdate, taskForEmployeeDb);
        await _repositoryManager.SaveAsync();
    }

    public async Task<(TasksForUpdateDto taskToPatch, Tasks taskEntity)> GetTaskForPatchAsync(
        Guid employeeId, Guid id, bool taskTrackChanges)
    {
        var taskForEmployeeDb = await GetTasksFromEmployeesAndCheckIfItExists(employeeId, id, taskTrackChanges);
        var taskToPatch = _mapper.Map<TasksForUpdateDto>(taskForEmployeeDb);
        return (taskToPatch, taskForEmployeeDb);
    }

    public async Task SaveChangesForPatchAsync(TasksForUpdateDto taskToPatch, Tasks taskEntity)
    {
        _mapper.Map(taskToPatch, taskEntity);
        await _repositoryManager.SaveAsync();
    }

    
    private async Task<Tasks> GetTasksFromEmployeesAndCheckIfItExists(Guid employeeId, Guid taskId, bool trackChanges)
    {
        var tasks = await _repositoryManager.Tasks.GetEmployeeTaskAsync(employeeId, taskId, trackChanges);
        if (tasks is null)
            throw new TasksNotFoundException(taskId);

        return tasks;
    }
}