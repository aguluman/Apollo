namespace Contracts;

public interface IRepositoryManager
{
    ICompanyRepository Company { get; }
    IEmployeeRepository Employee { get; }
    ITasksRepository Tasks { get; }
    Task SaveAsync();
}