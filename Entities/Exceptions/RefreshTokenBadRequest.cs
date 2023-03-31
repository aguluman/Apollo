namespace Entities.Exceptions;

public class RefreshTokenBadRequest : BadRequestException
{
    public RefreshTokenBadRequest() : base("Invalid request. The tokenDto has some invalid values")
    {
    }
}