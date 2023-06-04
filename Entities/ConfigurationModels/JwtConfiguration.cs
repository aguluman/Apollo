namespace Entities.ConfigurationModels;

public class JwtConfiguration
{
    public string Section { get; set; } = "JwtSetting";
    
    public string? ValidIssuer { get; set; }
    
    public string? ValidAudience { get; set; }
    
    public string? Expires { get; set; }
}