using Application.Services.User;
using Application.Services.User.Payload;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        private IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] UserPayload user)
        {
            var result = _userService.CreateAccountAsync(user);
            if (result == null || !result.Result)
            {
                return BadRequest();
            }   
            return Ok();
        }

        [HttpPost("Login")]
        public Task Login([FromBody] UserPayload user)
        {
            // Logic to log in a user
            return Task.CompletedTask;
        }
    }
}
