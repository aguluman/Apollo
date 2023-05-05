namespace Entities.Exceptions;

public class EmployeePositionNotFoundException : NotFoundException
{
    public EmployeePositionNotFoundException(string position) : base("Employee position not found.")
    {
    }
}