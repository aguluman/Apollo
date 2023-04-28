namespace Service.Contracts;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    IEmployeeService EmployeeService { get; }
    ITaskService TaskService { get; }
    IAuthenticationService AuthenticationService { get; }
}