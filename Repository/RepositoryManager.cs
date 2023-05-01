using Contracts;

namespace Repository;

public class RepositoryManager : IRepositoryManager
{
    //Todo add everything attendance here
    private readonly RepositoryContext _repositoryContext;
    private readonly Lazy<ICompanyRepository> _companyRepository;
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<ITasksRepository> _tasksRepository;
    private readonly Lazy<IAttendanceRepository> _attendanceRepository;

    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
        _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
        _tasksRepository = new Lazy<ITasksRepository>(() => new TasksRepository(repositoryContext));
        //Todo _attendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(repositoryContext));
        //Todo finish AttendanceRepository
    }

    public ICompanyRepository Company => _companyRepository.Value;
    public IEmployeeRepository Employee => _employeeRepository.Value;
    public ITasksRepository Tasks => _tasksRepository.Value;
    public IAttendanceRepository Attendance => _attendanceRepository.Value;

    public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
}