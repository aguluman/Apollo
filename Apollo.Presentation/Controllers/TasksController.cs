using System.Text.Json;
using Apollo.Presentation.ActionFilters;
using Entities.LinkModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Apollo.Presentation.Controllers;

[Route("api/employees/{employeeId:guid}/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    
    public TasksController(IServiceManager serviceManager) => 
        _serviceManager = serviceManager;
    
    [HttpGet]
    [HttpHead]
    [ServiceFilter(typeof(ValidateMediaTypeAttributes))]
    public async Task<IActionResult> GetTasksForEmployee(Guid employeeId, Guid taskId,
        [FromQuery] TasksParameters taskParameters)
    {
        var linkParams = new LinkParameters(null, taskParameters, HttpContext);
        
        var result = await _serviceManager.TaskService.GetEmployeesTasksAsync(
           employeeId, linkParams, false);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks
            ? Ok(result.linkResponse.LinkedEntities)
            : Ok(result.linkResponse.ShapedEntities);
    }
    
    [HttpGet("{taskId:guid}", Name = "GetTaskForEmployee")]
    public async Task<IActionResult> GetTaskForEmployee(Guid employeeId, Guid taskId)
    {
        var task = await _serviceManager.TaskService.GetEmployeeTaskAsync(employeeId, taskId, false);
        return Ok(task);
    }
    
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateTaskForEmployee(Guid employeeId, [FromBody] TasksForCreationDto task)
    {
        var taskToReturn = await _serviceManager.TaskService.CreateTaskForEmployeeAsync(
            employeeId, task, false);

        return CreatedAtRoute(
            "GetTaskForEmployee", 
            new { employeeId, taskId = taskToReturn.Id }, 
            taskToReturn);
    }
    
    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTaskForEmployee(Guid employeeId, Guid taskId)
    {
        await _serviceManager.TaskService.DeleteTaskForEmployeeAsync(employeeId, taskId, false);
        return NoContent();
    }
    
    [HttpPut("{taskId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateTaskForEmployee(Guid employeeId, Guid taskId,
        [FromBody] TasksForUpdateDto task)
    {
        await _serviceManager.TaskService.UpdateTaskForEmployeeAsync(
            employeeId, taskId, task, false,
            false);
        return NoContent();
    }
    
    [HttpPatch("{taskId:guid}")]
    public async Task<IActionResult> PartiallyUpdateTaskForEmployee(Guid employeeId, Guid taskId,
        [FromBody] JsonPatchDocument<TasksForUpdateDto> patchDoc)
    {
       if (patchDoc is null)
           return BadRequest("patch Document object sent from client is null");
       
       var result = await _serviceManager.TaskService.GetTaskForPatchAsync(
           employeeId, taskId, false, false);

       patchDoc.ApplyTo(result.taskToPatch, ModelState);

       TryValidateModel(result.taskToPatch);
       
       if(!ModelState.IsValid)
           return UnprocessableEntity(ModelState);

       await _serviceManager.TaskService.SaveChangesForPatchAsync(result.taskToPatch, result.taskEntity);
       
       return NoContent();
    }
}