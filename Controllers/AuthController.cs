using AutoMapper;
using Florin_API.DTOs;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers
{
    [Route("[controller]")]
    public class AuthController(IMapper mapper, IAuthService authService, IUserService userService, IRefreshTokenService refreshTokenService, ICurrentUserService currentUserService) : ApiBaseController
    {
        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerDTO">User registration data</param>
        /// <returns>The created user with their ID</returns>
        /// <response code="201">User created successfully</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="409">Email already exists</response>
        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = mapper.Map<User>(registerDTO);
            user.Password = registerDTO.Password;

            var userCreated = await authService.RegisterAsync(user);
            var userDTO = mapper.Map<UserDTO>(userCreated);

            return CreatedAtAction(nameof(Register), new { id = userDTO.Id }, userDTO);
        }

        /// <summary>
        /// Authenticates a user and returns access and refresh tokens
        /// </summary>
        /// <param name="loginDTO">Login credentials</param>
        /// <returns>The authenticated user with their tokens</returns>
        /// <response code="200">Login successful</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="401">Invalid credentials</response>
        [HttpPost(nameof(Login))]
        [ProducesResponseType(typeof(AuthResponseDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = mapper.Map<User>(loginDTO);
            user.Password = loginDTO.Password;

            var userLoggedIn = await authService.LoginAsync(user);
            var accessToken = authService.GenerateAccessToken(userLoggedIn);
            var refreshToken = await refreshTokenService.CreateRefreshTokenAsync(userLoggedIn);
            var authResponseDTO = new AuthResponseDTO
            {
                User = mapper.Map<UserDTO>(userLoggedIn),
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };

            return Ok(authResponseDTO);
        }

        /// <summary>
        /// Gets the currently authenticated user
        /// </summary>
        /// <returns>The current user information</returns>
        /// <response code="200">User information retrieved successfully</response>
        /// <response code="401">User not authenticated or invalid token</response>
        /// <response code="404">User not found</response>
        [HttpGet(nameof(Me))]
        [Authorize]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Me()
        {
            var user = await userService.GetUserByIdAsync(currentUserService.UserId);
            var userDTO = mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        /// <summary>
        /// Logs out the user by revoking their refresh token
        /// </summary>
        /// <param name="logoutDTO">Logout data containing the refresh token</param>
        /// <returns>No content on successful logout</returns>
        /// <response code="204">Logout successful</response>
        /// <response code="400">Invalid input data</response>
        /// <response code="401">User not authenticated or invalid token</response>
        [HttpDelete(nameof(Logout))]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout([FromBody] LogoutDTO logoutDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await userService.GetUserByIdAsync(currentUserService.UserId);
            await refreshTokenService.RevokeRefreshTokenAsync(logoutDTO.RefreshToken, user);

            return NoContent();
        }
    }
}
