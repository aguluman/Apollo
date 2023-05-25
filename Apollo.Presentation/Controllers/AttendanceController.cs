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

[Route("api/employees/{employeeId:guid}/attendances")]
[ApiController]
public class AttendanceController : ControllerBase
{
    //Todo : Add AttendanceController
    private readonly IServiceManager _serviceManager;

    public AttendanceController(IServiceManager serviceManager) =>
        _serviceManager = serviceManager;
    
    
}