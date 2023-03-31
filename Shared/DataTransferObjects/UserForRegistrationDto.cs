using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record UserForRegistrationDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    [Required(ErrorMessage = "UserName is required")]
    public string? UserName { get; init; }
    
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public ICollection<string> Roles { get; init; }
}