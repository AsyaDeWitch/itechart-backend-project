using BLL.Interfaces;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSenderService _emailSender;
        private readonly ITokenService _tokenService;
        private readonly IValidatorService _validatorService;

        public AuthService(IUnitOfWork unitOfWork, IEmailSenderService emailSender, ITokenService tokenService, IValidatorService validatorService)
        {
            _emailSender = emailSender;
            _tokenService = tokenService;
            _validatorService = validatorService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(userId);

            if (user == null)
            {
                return IdentityResult.Failed();
            }
            var result = await _unitOfWork.ExtendedUsers.ConfirmEmailAsync(user, token);
            return result;
        }

        public async Task<string> GenerateConfirmationLinkAsync(ExtendedUser user)
        {
            return await _unitOfWork.ExtendedUsers.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task SendConfirmationLinkAsync(string userId, string confirmationLink)
        {
            var htmlMessage = "<p><a href=\"" + confirmationLink + "\">Follow the link to confirm your email</a></p>";
            await _emailSender.SendEmailByMailKitAsync(userId, htmlMessage);
        }

        public async Task<(ExtendedUser, string)> SignInUserAsync(string email, string password, string issuer, string audience, string key)
        {
            if(!_validatorService.IsValidEmail(email))
            {
                return (null, null);
            }

            var user = await _unitOfWork.ExtendedUsers.FindByEmailAsync(email);
            if (user == null || !_validatorService.IsValidPassword(password))
            {
                return (null, null);
            }

            var signInResult = await _unitOfWork.ExtendedUsers.PasswordSignInAsync(user, password);
            if (!signInResult.Succeeded)
            {
                return (null, null);
            }

            var tokenString = _tokenService.BuildToken(user, issuer, audience, key);
            return tokenString == null ? (null, null) : (user, tokenString);
        }

        public async Task<ExtendedUser> SignUpUserAsync(string email, string password)
        {
            if(!_validatorService.IsValidEmail(email) || !_validatorService.IsValidPassword(password))
            {
                return null;
            }

            var existUser = await _unitOfWork.ExtendedUsers.FindByEmailAsync(email);
            if (existUser != null)
            {
                return null;
            }

            var user = _unitOfWork.ExtendedUsers.CreateForSignUp(email);
            var result = await _unitOfWork.ExtendedUsers.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return null;
            }

            await _unitOfWork.ExtendedUsers.AddToRoleAsync(user, "User");
            return user;
        }
    }
}
