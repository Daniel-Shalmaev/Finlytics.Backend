using System.Text;
using Microsoft.IdentityModel.Tokens;
using Finlytics.Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Finlytics.Infrastructure.Dependencies;

public static class JwtConfiguration
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // Load JWT settings from configuration (appsettings.json)
        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
        var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

        // Configure authentication scheme to use JWT bearer tokens
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            // Define how JWT tokens should be validated
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,                         // Validate token issuer
                ValidateAudience = true,                       // Validate token audience
                ValidateLifetime = true,                       // Validate expiration
                ValidateIssuerSigningKey = true,               // Validate the signature key
                ValidIssuer = jwtSettings.Issuer,              // Expected issuer
                ValidAudience = jwtSettings.Audience,          // Expected audience
                IssuerSigningKey = new SymmetricSecurityKey(   // Signing key from config
                    Encoding.UTF8.GetBytes(jwtSettings.Key)
                )
            };
        });

        return services;
    }
}
