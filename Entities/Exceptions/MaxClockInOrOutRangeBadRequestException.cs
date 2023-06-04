namespace Entities.Exceptions;

public class MaxClockInOrOutRangeBadRequestException : BadRequestException
{
    public MaxClockInOrOutRangeBadRequestException() 
        : base("Either MaxClockIn or MaxClockOut time is less than MinClockIn or MinCLockOut time in Attendance Parameters")
    {
    }
}