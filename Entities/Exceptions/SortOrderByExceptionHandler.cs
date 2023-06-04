namespace Entities.Exceptions;

public class SortOrderByExceptionHandler : NotFoundException
{
    public SortOrderByExceptionHandler() : base("The string to be used for ordering is missing or spelt incorrectly")
    {
    }
}