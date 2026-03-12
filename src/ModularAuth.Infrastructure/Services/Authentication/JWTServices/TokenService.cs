using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ModularAuth.Application.Interfaces;
using ModularAuth.Domain.Entities;
using ModularAuth.Infrastructure.Security;

namespace ModularAuth.Infrastructure.Services.Authentication.JWTServices;

public class TokenService : ITokenService
{
    // Get JWT Settings.
    private readonly JwtSettings _jwtSettings;

    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    
    // GenerateAccessToken()
    public string GenerateAccessToken(UserModel user)
    {
        // key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        // credentials
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // claims setup

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: credentials
        );

        

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // GenerateRefreshToken()
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
