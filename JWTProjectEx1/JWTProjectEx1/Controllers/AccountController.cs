using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace JWTProjectEx1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;
        private JwtSettings _jwtSettings;

        public AccountController(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpGet("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            var user = _userService.AuthenticateUser(loginModel.Username, loginModel.Password);

            if (user == null) 
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var claims = new List<Claim>();
            claims.Add(new Claim("username", user.Username));
            claims.Add(new Claim("displayname", user.Name));

            var token = JwtHelper.GetJwtToken(user.Username, _jwtSettings, new TimeSpan(0, 15, 0), claims.ToArray());
            return Ok(token);
        }
    }
}
