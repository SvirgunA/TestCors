using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using TestCors.Common.Settings;

namespace TestCors.API.Configure
{
    public static class Auth
    {
        public static void AddJwtAuth(this IServiceCollection services, AuthSettings authSettings)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((options) =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = authSettings.TOKEN_ISSUER,
                    ValidateAudience = true,
                    ValidAudiences = new[] { authSettings.TOKEN_AUDIENCE },
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1),
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.ENCRYPTION_KEY))
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
            });
        }
    }
}
