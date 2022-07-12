using ContactsService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Login;

namespace ContactsService.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IApiAuth _apiAuth;

        public LoginController(ILogger<LoginController> logger, IApiAuth apiAuth)
        {
            _logger = logger;
            _apiAuth = apiAuth;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login user)
        {
            if (user == null)
                return BadRequest("Invalid user");

            var tokenString = _apiAuth.AuthenticateUser(user.Username, user.Password);

            if (string.IsNullOrEmpty(tokenString))
                return Unauthorized();

            return Ok(new { Token = tokenString });
        }
    }
}
