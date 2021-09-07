﻿using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IEmailSenderService _emailSender;

        public JwtAuthService(UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager, IEmailSenderService emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);

                return result;
            }
            return IdentityResult.Failed();
        }

        public async Task<string> GenerateComfirmationLinkAsync(IdentityUser<int> user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public string GenerateJwt(IdentityUser<int> user, string jwtIssuer, string jwtAudience, string jwtKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creditals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: creditals
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task SendConfirmationLinkAsync(string userId, string confirmationLink)
        {
            string htmlMessage = "<p><a href=\"" + confirmationLink + "\">Follow the link to confirm your email</a></p>";
            await _emailSender.SendEmailByMailKitAsync(userId, htmlMessage);
        }

        public async Task<IdentityUser<int>> SignInUserAsync(string email, string password)
        {
            if(ValidatorService.IsValidEmail(email))
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    if (ValidatorService.IsValidPassword(password))
                    {
                        var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
                        if (signInResult.Succeeded)
                        {
                            return user;
                        }
                    }
                }
            }
            return null;
        }

        public async Task<IdentityUser<int>> SignUpUserAsync(string email, string password)
        {
            if(ValidatorService.IsValidEmail(email))
            { 
                if (ValidatorService.IsValidPassword(password))
                {
                    var user = new IdentityUser<int>
                    {
                        UserName = email,
                        Email = email,
                    };

                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        return user;
                    }
                }  
            }
            return null;
        }
    }
}
