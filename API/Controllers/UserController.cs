using Application.Services.User;
using Application.Services.User.Payload;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateAccount([FromBody] UserPayload user)
        {
            var result = _userService.CreateAccountAsync(user);
            if (!result.Result)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{cpf}")]
        public async Task<IActionResult> GetUserByIdentification([FromRoute] string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return BadRequest("CPF não pode ser vazio.");

            var payload = await _userService.IdentificationAsync(cpf);

            if (payload == null)
                return NotFound("Usuário não encontrado.");

            return Ok(payload);
        }
    }
}
