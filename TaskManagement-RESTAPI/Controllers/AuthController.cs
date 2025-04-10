using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Security;
using TaskManagement_RESTAPI.Services.Contracts;
using TaskManagement_RESTAPI.Shared.DTO;

namespace TaskManagement_RESTAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var user = await _serviceManager.UserService.AuthenticateUser(login);
            var accesstoken = new JwtTokenGenerator().GenerateToken(user);
            var refreshtoken = new JwtTokenGenerator().GenerateRefreshToken();
            user.RefreshToken = refreshtoken;
            user.ExpireTime = DateTime.UtcNow.AddDays(2);

            await _serviceManager.UserService.UpdateUserRefreshToken(user);

            return Ok(new { Message = "Login successful", accesstoken = accesstoken, refreshtoken = refreshtoken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToke(RequestDTO requestDTO)
        {
            var JwtTokenGenerator = new JwtTokenGenerator();
            Console.WriteLine("came 1");
            var principal = JwtTokenGenerator.GetPrincipalFromExpired(requestDTO.AccessToken ?? string.Empty);
            Console.WriteLine("came 2");

            var username = principal?.Identity?.Name;
            Console.WriteLine("came 3");

            var user = await _serviceManager.UserService.GetUserByUsername(username ?? string.Empty);
            Console.WriteLine(user.RefreshToken);
            Console.WriteLine(requestDTO.RefreshToken);
            if (user.RefreshToken != requestDTO.RefreshToken || user.ExpireTime <= DateTime.UtcNow)
            {
                throw new SecurityTokenException("Invalid refresh token.");
            }

            var AccessToken = JwtTokenGenerator.GenerateToken(user);
            var RefreshToken = JwtTokenGenerator.GenerateRefreshToken();

            user.RefreshToken = RefreshToken;
            user.ExpireTime = DateTime.UtcNow.AddDays(2);

            await _serviceManager.UserService.UpdateUserRefreshToken(user);

            return Ok(new {accesstoke = AccessToken, refreshtoken = RefreshToken});
        }
    }
}
