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
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
        {
            var userToRegister = UserMapper.ToEntity(registerDto);
            var registeredUser = await authService.RegisterAsync(userToRegister);
            var userDto = UserMapper.ToDTO(registeredUser);

            return CreatedAtAction(
                nameof(UserController.GetUserById),
                "User",
                new { id = userDto.Id },
                userDto
            );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var userToLogIn = UserMapper.ToEntity(loginDto);
            var loggedInUser = await authService.LoginAsync(userToLogIn, loginDto.RememberMe);
            var userDto = UserMapper.ToDTO(loggedInUser);

            return Ok(userDto);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = userContextService.GetCurrentUserId();
            var currentUser = await userService.GetUserByIdAsync(userId);
            var userDto = UserMapper.ToDTO(currentUser);

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
