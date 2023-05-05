namespace Entities.Exceptions;

public class MaxDateRangeBadRequestException : BadRequestException
{
    public MaxDateRangeBadRequestException() : base("EndDate can not be less than StartDate")
    {
    }
}