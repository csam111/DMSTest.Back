using DMSTest.Back.Services;
using DMSTest.DTO.Request;
using DMSTest.DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DMSTest.Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly DMSTest.BAL.Account _account;
        private readonly IUserService _userService;

        private Answer answer = new Answer();
        public AccountController(DMSTest.BAL.Account account, IConfiguration configuration, IUserService userService)
        {
            _account = account;
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] AuthRequest User)
        {

            var userResponse = _userService.Auth(User);
            if (userResponse.User == null)
            {
                answer.Success = 0;
                answer.Message = "Usuario o Contrasena Incorrecta";
                return BadRequest(answer);
            }

            answer.Success = 1;
            answer.Data = userResponse;
            return Ok(answer);
        }
    }
}
