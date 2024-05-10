using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ManageActivities.Configuration.Authentication;

public static class JwtProvider
{
    public static void AddJwt(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        const string bearerDefaults = JwtBearerDefaults.AuthenticationScheme;
        serviceCollection.AddAuthentication(bearerDefaults).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = GetJwtValidationParameters(configuration);
        });
    }

    private static TokenValidationParameters GetJwtValidationParameters(IConfiguration configuration)
    {
        IConfigurationSection jwtSection = configuration.GetSection("Jwt");
        string? issuer = jwtSection["Issuer"];
        string? audience = jwtSection["Audience"];
        string? jwtKey = jwtSection["Key"];

        if (audience is null || issuer is null || jwtKey is null)
        {
            throw new ApplicationException();
        }

        SymmetricSecurityKey jwtSymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = jwtSymmetricSecurityKey
        };
    }
}