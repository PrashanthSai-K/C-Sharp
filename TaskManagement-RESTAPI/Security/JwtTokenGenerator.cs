using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaskManagement_RESTAPI.Entities.Models;
using TaskManagement_RESTAPI.Exceptions;

namespace TaskManagement_RESTAPI.Security;

public class JwtTokenGenerator
{
    private readonly string jwtKey = Environment.GetEnvironmentVariable("JWTKEY") ?? throw new Exception("JWT Key environment not set");
    private readonly string api = Environment.GetEnvironmentVariable("API") ?? throw new Exception("API environment variable not set");
    private readonly SymmetricSecurityKey key;
    public JwtTokenGenerator()
    {
        key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
    }

    public string GenerateRefreshToken()
    {
        var randBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randBytes);
        return Convert.ToBase64String(randBytes);
    }

    public ClaimsPrincipal? GetPrincipalFromExpired(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            ValidIssuer = api,
            ValidAudience = api,
            IssuerSigningKey = key
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null
        || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
        StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public TokenValidationParameters TokenValidationParameters()
    {
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
