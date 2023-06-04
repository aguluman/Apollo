using Apollo.Presentation.ActionFilters;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using Swashbuckle.AspNetCore.Annotations;

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
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetTasksForEmployee(Guid employeeId,
        [FromQuery] TasksParameters taskParameters)
    {
        var tasksLinkParams = new TasksLinkParameters(taskParameters, HttpContext);

        var result = await _serviceManager.TasksService.GetEmployeeTasksAsync(employeeId,
            tasksLinkParams, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks
            ? Ok(result.linkResponse.LinkedEntities)
            : Ok(result.linkResponse.ShapedEntities);
    }

    [HttpGet("{taskId:guid}", Name = "GetTaskForEmployee")]
    public async Task<IActionResult> GetTaskForEmployee(Guid employeeId, Guid taskId)
    {
        var task = await _serviceManager.TasksService.GetEmployeeTaskAsync(employeeId, taskId, false);
        return Ok(task);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateTaskForEmployee(Guid employeeId,
        [FromBody] TasksForCreationDto task)
    {
        var taskToReturn = await _serviceManager.TasksService.CreateTaskForEmployeeAsync(
            employeeId, task);

        return CreatedAtRoute(
            "GetTaskForEmployee",
            new { employeeId, taskId = taskToReturn.Id },
            taskToReturn);
    }

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> DeleteTaskForEmployee(Guid employeeId, Guid taskId)
    {
        await _serviceManager.TasksService.DeleteTaskForEmployeeAsync(employeeId, taskId, false);
        return NoContent();
    }

    [HttpPut("{taskId:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateTaskForEmployee(Guid employeeId, Guid taskId,
        [FromBody] TasksForUpdateDto tasksForUpdate)
    {
        await _serviceManager.TasksService.UpdateTaskForEmployeeAsync(
            employeeId, taskId, tasksForUpdate, true);
        return NoContent();
    }

    [HttpPatch("{taskId:guid}")]
    [SwaggerOperation(
        Summary = "Partially updates a task for an employee.",
        Description = "Applies a JSON Patch document to partially update a task for an employee. Like this:" +
                      "[{ op: 'replace', path: '/property name', value: 'new value' }]" +
                      "The 'op', 'path' and 'value' properties are required are declared as strings. " +
                      "Also the 'operation', '/property name' and 'value' to apply are required and are declared as strings too.",
        OperationId = "PartiallyUpdateTaskForEmployee",
        Tags = new[] { "Tasks" })]
    public async Task<IActionResult> PartiallyUpdateTaskForEmployee(Guid employeeId, Guid taskId,
        [FromBody] JsonPatchDocument<TasksForUpdateDto>? patchDoc)
    {
        if (patchDoc is null)
            return BadRequest("patch Document object sent from client is null");

        var result = await _serviceManager.TasksService.GetTaskForPatchAsync(
            employeeId, taskId, true);

        patchDoc.ApplyTo(result.taskToPatch, ModelState);

        TryValidateModel(result.taskToPatch);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.TasksService.SaveChangesForPatchAsync(result.taskToPatch, result.taskEntity);

        return NoContent();
    }
}