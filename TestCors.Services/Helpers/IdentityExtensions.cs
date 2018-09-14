using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TestCors.Common.Settings;
using TestCors.Models;

namespace TestCors.Services.Helpers
{
    public static class IdentityExtensions
    {
        public static ClaimsIdentity GenerateIdentity(this User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName)
            };

            //if (user.Roles != null)
            //{
            //    claims.AddRange(user.Roles.Select(r => new Claim("roles", r.Role.ToString())));
            //}

            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        }

        public static string GenerateIdentityToken(this ClaimsIdentity identity, AuthSettings settings)
        {
            DateTime now = DateTime.UtcNow;

            JwtSecurityToken jwtToken = new JwtSecurityToken(
                issuer: settings.TOKEN_ISSUER,
                audience: settings.TOKEN_AUDIENCE,
                claims: identity.Claims,
                notBefore: now,
                expires: now.AddMinutes(settings.ID_TOKEN_LIFETIME_MINUTES),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.ENCRYPTION_KEY)),
                    SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
