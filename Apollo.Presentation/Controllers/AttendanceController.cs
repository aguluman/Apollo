using Apollo.Presentation.ActionFilters;
using System.Text.Json;
using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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
    public async Task<IActionResult> CreateClockInForEmployee(Guid employeeId,
        [FromBody] AttendanceForClockInDto attendance)
    {
        var attendanceToReturn = await _serviceManager.AttendanceService
            .CreateClockInForAttendance(employeeId, attendance);

        return CreatedAtRoute(
            "GetAttendanceForEmployee",
            new { employeeId, attendanceId = attendanceToReturn.Id },
            attendanceToReturn);
    }
    
    [HttpPost("{attendanceId:guid}/clock-out",  Name = "CreateClockOutForEmployee")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateClockOutForEmployee(Guid employeeId, Guid attendanceId,
        [FromBody] AttendanceForClockOutDto attendance)
    {
        var attendanceToReturn = await _serviceManager.AttendanceService
            .CreateClockOutForAttendance(employeeId, attendance.AttendanceId = attendanceId, attendance);

        return  CreatedAtRoute(
            "CreateClockOutForEmployee",
            new { employeeId, attendanceId = attendanceToReturn.Id },
            attendanceToReturn);
    }
    
    [HttpPost("{attendanceId:guid}/break-time-clock-in",  Name = "CreateBreakTimeClockInForEmployee")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateBreakTimeClockInForEmployee(Guid employeeId, Guid attendanceId,
        [FromBody] AttendanceForBtClockInDto attendance)
    {
        var attendanceToReturn = await _serviceManager.AttendanceService
            .CreateBreakTimeClockIn(employeeId, attendance.AttendanceId = attendanceId, attendance);

        return  CreatedAtRoute(
            "CreateBreakTimeClockInForEmployee",
            new { employeeId, attendanceId = attendanceToReturn.Id },
            attendanceToReturn);
    }
    
    [HttpPost("{attendanceId:guid}/break-time-clock-out",  Name = "CreateBreakTimeClockOutForEmployee")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateBreakTimeClockOutForEmployee(Guid employeeId, Guid attendanceId,
        [FromBody] AttendanceForBtClockOutDto attendance)
    {
        var attendanceToReturn = await _serviceManager.AttendanceService
            .CreateBreakTimeClockOut(employeeId, attendance.AttendanceId = attendanceId, attendance);

        return  CreatedAtRoute(
            "CreateBreakTimeClockOutForEmployee",
            new { employeeId, attendanceId = attendanceToReturn.Id },
            attendanceToReturn);
    }
}