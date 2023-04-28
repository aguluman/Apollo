namespace Entities.Exceptions;

public class MaxTimeRangeBadRequestException : BadRequestException
{
    public MaxTimeRangeBadRequestException() : base("MaxTime can not be less than MinTime")
    {
    }
}