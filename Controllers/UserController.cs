using Florin_API.Mappers;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            var userDto = UserMapper.ToDto(user);

            return Ok(userDto);
        }
    }
}
