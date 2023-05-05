using Apollo.Presentation.ActionFilters;
using Entities.LinkModels;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Apollo.Presentation.Controllers;

[Route("api/company/{companyId:guid}/attendance")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AttendanceController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;

    
    [HttpGet]
    [HttpHead]
    [ServiceFilter(typeof(ValidateMediaTypeAttributes))]
    public async Task<IActionResult> GetEmployeesAttendanceByCompanyId(
        Guid companyId, [FromQuery] AttendanceParameters attendanceParameters)
    {
        var linkParams = new LinkParameters(null, null, attendanceParameters, HttpContext);

        var result = await _serviceManager.AttendanceService.GetEmployeesAttendancesByCompanyIdAsync(
            companyId, linkParams, false);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Item2));
        
        return result.linkResponse.HasLinks
            ? Ok(result.linkResponse.LinkedEntities)
            : Ok(result.linkResponse.ShapedEntities);
    }

    [HttpGet("employee/{employeeId:guid}", Name = "Get All Attendance By One Employee")]
    public async Task<IActionResult> GetEmployeeAttendances(Guid employeeId,
        [FromQuery] AttendanceParameters attendanceParameters)
    {
        var linkParams = new LinkParameters(null, null, attendanceParameters, HttpContext);

        var result = await _serviceManager.AttendanceService
            .GetEmployeeAttendancesAsync(employeeId, linkParams, false);
        
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(result.Item2));

        return result.linkResponse.HasLinks
            ? Ok(result.linkResponse.LinkedEntities)
            : Ok(result.linkResponse.ShapedEntities);

    }
    

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateAttendanceForEmployee(
        Guid employeeId, [FromBody] AttendanceForCreationDto attendance, Guid companyId)
    {
        var attendanceToReturn = await _serviceManager.AttendanceService.CreateAttendanceForEmployeesAsync(
            employeeId, attendance, false);
        
        return CreatedAtRoute(
            "GetEmployeeAttendances+",
            new { employeeId, attendanceId = attendanceToReturn.Id },
            attendanceToReturn);
    }
}