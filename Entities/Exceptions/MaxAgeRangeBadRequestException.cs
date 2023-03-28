namespace Entities.Exceptions;

public sealed class MaxAgeRangeBadRequestException : BadRequestException
{
    public MaxAgeRangeBadRequestException() : base("MaxAge can not be less than MinAge")
    {
        
    }
}