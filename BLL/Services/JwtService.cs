using BLL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using RIL.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BLL.Services
{
    public class JwtService : ITokenService
    {
        private readonly int _expiryDurationInMinites = 120;
        public string BuildToken(ExtendedUser user, string jwtIssuer, string jwtAudience, string jwtKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creditals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_expiryDurationInMinites),
                signingCredentials: creditals
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ExtractUserIdFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            var id = jwtToken.Claims.First(claim => claim.Type == "UserId").Value;

            return id;
        }

        public bool IsTokenValid(string jwtIssuer, string jwtAudience, string jwtKey, string jwtToken)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                jwtTokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtIssuer,
                    ValidAudience = jwtAudience,
                    IssuerSigningKey = securityKey,
                }, 
                out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
