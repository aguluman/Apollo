namespace Entities.Exceptions;

public class EmployeeAttendanceNotFoundException : BadRequestException
{
    public EmployeeAttendanceNotFoundException() : base("EmployeeId and AttendanceId is not found!")
    {
    }
}