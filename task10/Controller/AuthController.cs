using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using task10.Contarcts;
using task10.Interface;
using task10.Models;

namespace task10.Contrroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IUserRepository userRepo, ILogger<AuthController> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUser user)
        {
            await _userRepo.CreateUser(user);
            return Ok("User created Successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userRepo.GetByName(username) ?? throw new Exception("User Not found");

            if (user.Password != password)
            {
                throw new Exception("Password mismatch");
            }

            var token = GenerateToken(user);

            _logger.LogInformation("☑️Log in successful..!!");
            return Ok(new {Message = "Login successful", token = token});
        }

        private string GenerateToken(User user)
        {
            var jwtKey = Environment.GetEnvironmentVariable("JWTKEY") ?? throw new Exception("JWTKEY environment variable is not set");
            var api = Environment.GetEnvironmentVariable("API") ?? throw new Exception("API enviromnet variable is not set");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: api,
                audience: api,
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: cred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
