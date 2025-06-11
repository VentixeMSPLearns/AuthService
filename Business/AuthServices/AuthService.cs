using Microsoft.AspNetCore.Identity;

namespace Business.AuthServices;
public class AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly SignInManager<IdentityUser> _signInManager = signInManager;

    public async Task<IdentityResult> SingUpAsync(string username, string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = username,
            Email = email,
        };
        var result = await _userManager.CreateAsync(user, password);
        return result;
    }
    public async Task<SignInResult> SignInAsync(string username, string password, bool isPersistent = false)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, false);
        return result;
    }
    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
