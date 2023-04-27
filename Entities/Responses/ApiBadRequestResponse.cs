namespace Entities.Responses;

public abstract class ApiBadRequestResponse : ApiBasedResponses
{
    public string Message { get; set; }

    public ApiBadRequestResponse(string message) : base(false)
    {
        Message = message;
    }
}