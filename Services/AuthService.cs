using System.Security.Claims;
using Florin_API.Entities;
using Florin_API.Exceptions.Auth;
using Florin_API.Exceptions.Http;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Florin_API.Services;

public class AuthService(IHttpContextAccessor httpContextAccessor, IUserService userService) : IAuthService
{
    public async Task<User> RegisterAsync(User user, CancellationToken cancellationToken)
    {
        return await userService.CreateUserAsync(user, cancellationToken);
    }

    public async Task<User> LoginAsync(User user, bool rememberMe, CancellationToken cancellationToken)
    {
        User? existingUser = await userService.GetUserByEmailAsync(user.Email, cancellationToken);
        if (existingUser is null || !userService.VerifyUserPassword(existingUser, user.Password, cancellationToken))
        {
            throw new BadCredentialsException();
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = rememberMe,
            AllowRefresh = true
        };

        var HttpContext = httpContextAccessor.HttpContext ?? throw new HttpContextException();
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        return existingUser;
    }

    public async Task LogoutAsync()
    {
        var HttpContext = httpContextAccessor.HttpContext ?? throw new HttpContextException();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
