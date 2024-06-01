using CodeCampus.Infrastructure.Entities;
using CodeCampus.Infrastructure.Interfaces.Services.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeCampus.Infrastructure.Services.Admin;

public class TokenService(IConfiguration config, UserManager<UserEntity> userManager, ILogger<TokenService> logger) : ITokenService
{
    private readonly IConfiguration _config = config;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly ILogger<TokenService> _logger = logger;


    public async Task<string> GenerateToken(UserEntity user)
    {
        try
        {
            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id)
        };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                   issuer: _config["Jwt:Issuer"],
                   audience: _config["Jwt:Audience"],
                   claims: claims,
                   expires: DateTime.Now.AddMinutes(30),
                   signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while generating the token.");
            throw;
        }
    }
}
