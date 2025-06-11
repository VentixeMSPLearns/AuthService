using Business.AuthServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Presentation.DTOs;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly IAuthService _authService = authService;
    private readonly UserManager<IdentityUser> _userManager = userManager;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(SignupRequest request)
    {
        try
        {
            var result = await _authService.SingUpAsync(request.Username, request.Email, request.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }
            var user = await _userManager.FindByNameAsync(request.Username);
            return Ok(new
            {
                message = "User signed up successfully",
                userId = user.Id,
                username = user.UserName,
                email = user.Email
            });
        }
        catch (Exception ex)
        {
            return Problem($"An unexpected error occurred: {ex.Message}.");
        }
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(SigninRequest request)
    {
        try
        {
            var result = await _authService.SignInAsync(request.Username, request.Password, request.IsPersistent);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid email or password.");
            }

            var user = await _userManager.FindByNameAsync(request.Username);
            return Ok(new
            {
                message = "User signed in successfully",
                userId = user.Id,
                username = user.UserName,
                email = user.Email
            });
        }
        catch (Exception ex)
        {
            return Problem($"An unexpected error occurred: {ex.Message}.");
        }
    }

    [HttpPost("signout")]
    public async Task<IActionResult> SignOutUser()
    {
        try
        {
            await _authService.SignOutAsync();
            return Ok("User logged out.");
        }
        catch (Exception ex)
        {
            return Problem($"An unexpected error occurred: {ex.Message}.");
        }
    }
}

