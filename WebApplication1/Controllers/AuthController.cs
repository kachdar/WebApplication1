using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Dtos.UserDtos;
using WebApplication1.Services.UserService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<GetUserDto>> Register(AddUserDto newUser)
        {
            var result = await _userService.CreateUser(newUser);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(AddUserDto user)
        {
            var result = await _userService.Login(user);
            return Ok(result);
        }
    }
}
