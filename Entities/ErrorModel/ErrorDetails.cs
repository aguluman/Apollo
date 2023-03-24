using System.Text.Json;

namespace Entities.ErrorModel;

public class ErrorDetails
{
    public int StatusCodes { get; set; }
    public string? Message { get; set; }

    public override string ToString() => JsonSerializer.Serialize(this);
}