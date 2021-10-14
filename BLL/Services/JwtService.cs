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
        private const int ExpiryDurationInMinutes = 120;
        public string BuildToken(ExtendedUser user, string jwtIssuer, string jwtAudience, string jwtKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(ExpiryDurationInMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ExtractUserIdFromToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            string id;
            try
            {
                var jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                id = jwtToken.Claims.First(claim => claim.Type == "UserId").Value;
            }
            catch (Exception e)
            {
                id = null;
            }
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
                out var validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
