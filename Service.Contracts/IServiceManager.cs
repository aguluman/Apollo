namespace Service.Contracts;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    IEmployeeService EmployeeService { get; }
    ITasksService TasksService { get; }
    IAuthenticationService AuthenticationService { get; }
}