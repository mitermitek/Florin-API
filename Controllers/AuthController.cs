using Florin_API.DTOs.Requests;
using Florin_API.Mappers;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IUserContextService userContextService, IAuthService authService, IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest, CancellationToken cancellationToken)
        {
            var userToRegister = UserMapper.ToEntity(registerRequest);
            var registeredUser = await authService.RegisterAsync(userToRegister, cancellationToken);
            var userResponse = UserMapper.ToResponse(registeredUser);
            return CreatedAtAction(
                nameof(UserController.GetUserById),
                "User",
                new { id = userResponse.Id },
                userResponse
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest, CancellationToken cancellationToken)
        {
            var userToLogIn = UserMapper.ToEntity(loginRequest);
            var loggedInUser = await authService.LoginAsync(userToLogIn, loginRequest.RememberMe, cancellationToken);
            var userResponse = UserMapper.ToResponse(loggedInUser);
            return Ok(userResponse);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var currentUser = await userService.GetUserByIdAsync(userId, cancellationToken);
            var userResponse = UserMapper.ToResponse(currentUser);

            return Ok(userResponse);
        }

        [HttpDelete("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await authService.LogoutAsync();

            return NoContent();
        }
    }
}
