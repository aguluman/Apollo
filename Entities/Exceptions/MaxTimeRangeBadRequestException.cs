namespace Entities.Exceptions;

public class MaxTimeRangeBadRequestException : BadRequestException
{
    public MaxTimeRangeBadRequestException() : base(
        "MaxTime set for TasksParameters can not be less than MinTime TasksParameters")
    {
        
    }
}