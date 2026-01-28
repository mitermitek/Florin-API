using Florin_API.DTOs.Auth;
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
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto, CancellationToken cancellationToken)
        {
            var userToRegister = UserMapper.ToEntity(registerDto);
            var registeredUser = await authService.RegisterAsync(userToRegister, cancellationToken);
            var userDto = UserMapper.ToDto(registeredUser);
            return CreatedAtAction(
                nameof(UserController.GetUserById),
                "User",
                new { id = userDto.Id },
                userDto
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto, CancellationToken cancellationToken)
        {
            var userToLogIn = UserMapper.ToEntity(loginDto);
            var loggedInUser = await authService.LoginAsync(userToLogIn, loginDto.RememberMe, cancellationToken);
            var userDto = UserMapper.ToDto(loggedInUser);
            return Ok(userDto);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var currentUser = await userService.GetUserByIdAsync(userId, cancellationToken);
            var userDto = UserMapper.ToDto(currentUser);

            return Ok(userDto);
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
