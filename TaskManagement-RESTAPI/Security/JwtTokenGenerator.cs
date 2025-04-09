using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagement_RESTAPI.Entities.Models;

namespace TaskManagement_RESTAPI.Security;

public class JwtTokenGenerator
{

    public TokenValidationParameters TokenValidationParameters()
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWTKEY") ?? throw new Exception("JWT Key environment not set");
        var api = Environment.GetEnvironmentVariable("API") ?? throw new Exception("API environment variable not set");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = api,
            ValidAudience = api,
            IssuerSigningKey = key
        };
    }

    public string GenerateToken(User user)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWTKEY") ?? throw new Exception("JWT Key environment not set");
        var api = Environment.GetEnvironmentVariable("API") ?? throw new Exception("API environment variable not set");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username ?? string.Empty),
            new Claim(ClaimTypes.Role, user.Role ?? string.Empty)
        };

        var token = new JwtSecurityToken
        (
            issuer: api,
            audience: api,
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: cred
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
