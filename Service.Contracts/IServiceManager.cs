namespace Service.Contracts;

public interface IServiceManager
{
    ICompanyService CompanyService { get; }
    IEmployeeService EmployeeService { get; }
    ITaskService TaskService { get; }
    IAttendanceService AttendanceService { get;  }
    IAuthenticationService AuthenticationService { get; }
}