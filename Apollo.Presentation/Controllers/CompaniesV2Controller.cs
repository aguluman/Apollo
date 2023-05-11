using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Apollo.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
public class CompaniesV2Controller : ControllerBase
{
    private readonly IServiceManager _service;
    
    public CompaniesV2Controller(IServiceManager service) =>
        _service = service;
    
    [HttpGet(Name = "GetCompaniesV2")]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _service.CompanyService.GetAllCompaniesAsync(false);
        return Ok(companies);
    }
}