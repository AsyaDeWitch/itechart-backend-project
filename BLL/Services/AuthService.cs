﻿using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly SignInManager<ExtendedUser> _signInManager;
        private readonly IEmailSenderService _emailSender;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ExtendedUser> userManager, SignInManager<ExtendedUser> signInManager, IEmailSenderService emailSender, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _tokenService = tokenService;
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

        public async Task<string> GenerateComfirmationLinkAsync(ExtendedUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task SendConfirmationLinkAsync(string userId, string confirmationLink)
        {
            string htmlMessage = "<p><a href=\"" + confirmationLink + "\">Follow the link to confirm your email</a></p>";
            await _emailSender.SendEmailByMailKitAsync(userId, htmlMessage);
        }

        public async Task<(ExtendedUser, string)> SignInUserAsync(string email, string password, string issuer, string audience, string key)
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
                            var tokenString = _tokenService.BuildToken(user, issuer, audience, key);
                            if(tokenString != null)
                            {
                                return (user, tokenString);
                            }
                        }
                    }
                }
            }
            return (null, null);
        }

        public async Task<ExtendedUser> SignUpUserAsync(string email, string password)
        {
            if(ValidatorService.IsValidEmail(email))
            { 
                if (ValidatorService.IsValidPassword(password))
                {
                    var user = new ExtendedUser
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
