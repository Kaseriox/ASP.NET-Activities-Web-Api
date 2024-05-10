using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManageActivities.Entity.Model;
using Microsoft.IdentityModel.Tokens;

namespace ManageActivities.Utils;

public static class JwtHandler
{
    public static string GenerateJwtToken(User user, IConfiguration configuration)
    {
        IConfigurationSection jwtSection = configuration.GetSection("Jwt");
        string? jwtKey = jwtSection["Key"];
        string? issuer = jwtSection["Issuer"];
        string? audience = jwtSection["Audience"];
        if (jwtKey is null || issuer is null || audience is null)
        {
            throw new ApplicationException();
        }

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        Claim[] claims =
        [
            new Claim("userId", user.Id.ToString())
        ];
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            null,
            DateTime.Now.AddMinutes(60),
            signingCredentials
        );
        return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    }
}