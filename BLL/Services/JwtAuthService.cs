using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JwtAuthService : IJwtAuthService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly IEmailSenderService _emailSender;

        public JwtAuthService(IConfiguration config, UserManager<IdentityUser<int>> userManager, SignInManager<IdentityUser<int>> signInManager, IEmailSenderService emailSender)
        {
            _config = config;
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

        public string GenerateJwt(IdentityUser<int> user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creditals = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
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
            if(IsValidEmail(email))
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user != null)
                {
                    if (IsValidPassword(password))
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
            if(IsValidEmail(email))
            { 
                if (IsValidPassword(password))
                {
                    var user = new IdentityUser<int>
                    {
                        UserName = email,
                        Email = email,
                    };

                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        return user;
                    }
                }  
            }
            return null;
        }

        private bool IsValidEmail(string email)
        {
            if(string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            try
            {
                //min 1 lowercase, 1 uppercase, 1 number, 1 special character (non-letter and non-number)
                //8+ characters
                return Regex.IsMatch(password,
                    @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!*()@%&])[\s\S]{8,}$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch(RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
