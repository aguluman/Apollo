namespace Entities.Responses;

public abstract class ApiBasedResponses
{
    public bool Success { get; set; }

    public ApiBasedResponses(bool success) => Success = success;
}