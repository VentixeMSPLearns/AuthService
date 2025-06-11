namespace Presentation.DTOs;

public class SigninRequest
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool IsPersistent { get; set; } = false;
}
