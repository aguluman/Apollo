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

[Route("api/attendances/employees/{employeeId:guid}")]
[ApiController]
public class AttendanceController : ControllerBase
{
    //Todo : Add AttendanceController
    private readonly IServiceManager _serviceManager;

    public AttendanceController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;
    
    [HttpGet]
    [HttpHead]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetAttendancesForEmployee(Guid employeeId,
        [FromQuery] AttendanceParameters attendanceParameters)
    {
        var attendanceLinkParams = new AttendanceLinkParameters(attendanceParameters, HttpContext);

        var result = await _serviceManager.AttendanceService
            .GetEmployeeAttendancesAsync(employeeId,
            attendanceLinkParams, false);

        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.metaData));

        return result.linkResponse.HasLinks
            ? Ok(result.linkResponse.LinkedEntities)
            : Ok(result.linkResponse.ShapedEntities);
    }
    
    [HttpGet("{attendanceId:guid}", Name = "GetAttendanceForEmployee")]
    public async Task<IActionResult> GetAttendanceForEmployee(Guid employeeId, Guid attendanceId)
    {
        var attendance = await _serviceManager.AttendanceService
            .GetEmployeeAttendanceAsync(employeeId, attendanceId, false);
        return Ok(attendance);
    }

    /// <summary>
    /// Creates a new attendance record for an employee
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="attendance"></param>
    /// <returns>A newly created attendance record</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="422">If the model is invalid</response>
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateAttendanceForEmployee(Guid employeeId,
        [FromBody] AttendanceForCreationDto attendance)
    {
        var attendanceToReturn = await _serviceManager.AttendanceService
            .CreateAttendanceForEmployeeAsync(employeeId, attendance);

        return CreatedAtRoute(
            "GetAttendanceForEmployee",
            new { employeeId, attendanceId = attendanceToReturn.Id },
            attendanceToReturn);
    }
}