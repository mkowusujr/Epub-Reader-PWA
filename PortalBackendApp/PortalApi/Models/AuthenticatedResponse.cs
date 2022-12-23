namespace PortalApi.Models;

public class AuthenticatedResponse
{
    public string? Token { get; set; }
    public User? UserData { get; set; }
}