namespace Entities.Exceptions;

public class AttendanceNotFoundException : NotFoundException
{
    public AttendanceNotFoundException(Guid attendanceId) 
        : base($"Attendance with {attendanceId} does not exist in the database.")
    {
    }
}