using AutoMapper;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Service.Contracts;

namespace Service;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;
    private readonly Lazy<ITaskService> _taskService;
    private readonly Lazy<IAuthenticationService> _authenticationService;

    public ServiceManager(
        IRepositoryManager repositoryManager, 
        ILoggerManager logger, 
        IMapper mapper, 
        IEmployeeLinks employeeLinks,
        ITaskLinks taskLinks,
        UserManager<User> userManager,
        IOptionsMonitor<JwtConfiguration> configuration)
    {
        _companyService = new Lazy<ICompanyService>(() =>
            new CompanyService(repositoryManager, logger, mapper));
        
        _employeeService = new Lazy<IEmployeeService>(() =>
            new EmployeeService(repositoryManager, logger, mapper, employeeLinks));

        _taskService = new Lazy<ITaskService>(() =>
            new TasksService(repositoryManager, logger, mapper, taskLinks));
        
        _authenticationService = new Lazy<IAuthenticationService>(() =>
            new AuthenticationService(logger, mapper, userManager, configuration));

    }

    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
    public ITaskService TaskService => _taskService.Value;
    public IAuthenticationService AuthenticationService => _authenticationService.Value;
}