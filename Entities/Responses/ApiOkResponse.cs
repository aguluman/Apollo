namespace Entities.Responses;

public sealed class ApiOkResponse<TResult> : ApiBasedResponses
{
    public TResult Result { get; set; }
    
    public ApiOkResponse(TResult result) : base(true)
    {
        Result = result;
    }
}