using Microsoft.AspNetCore.Identity;

namespace Business.AuthServices
{
    public interface IAuthService
    {
        Task<SignInResult> SignInAsync(string email, string password, bool isPersistent = false);
        Task SignOutAsync();
        Task<IdentityResult> SingUpAsync(string username, string email, string password);
    }
}