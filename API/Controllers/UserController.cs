using Application.Services.User;
using Application.Services.User.Payload;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
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

        [HttpPost("Identification")]
        public Task<string> Login([FromBody] UserPayload user)
        {
            if (user == null || string.IsNullOrEmpty(user.CPF))
            {
                return Task.FromResult("Invalid user data.");
            }
            var userPayload = _userService.IdentificationAsync(user);
            if (userPayload.Result == null)
            {
                return Task.FromResult("User not found.");
            }

            return Task.FromResult(userPayload.Result.Name);
        }
    }
}
