using BLL.Dto;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using RIL.Models;
using System;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly AddressDto _addressDto;

        public UserService(ITokenService tokenService, UserManager<ExtendedUser> userManager, ApplicationDbContext context)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _context = context;
            _addressDto = new AddressDto(_context);
        }

        public string GetUserId(string token)
        {
            return _tokenService.ExtractUserIdFromToken(token);
        }

        public async Task<ReturnUserProfileViewModel> UpdateUserProfile(UserProfileViewModel userProfile, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (!String.IsNullOrWhiteSpace(userProfile.PhoneNumber))
                {
                    if (ValidatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                    {
                        user.PhoneNumber = userProfile.PhoneNumber;

                    }
                    else
                        return null; 
                }

                if (!String.IsNullOrWhiteSpace(userProfile.UserName))
                {
                    user.UserName = userProfile.UserName;
                }

                if (userProfile.AddressDelivery != null)
                {
                    user.AddressDelivery = userProfile.AddressDelivery;
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ReturnUserProfileViewModel
                    {
                        AddressDelivery = user.AddressDelivery,
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                    };
                }

            }
            return null;
        }

        public async Task<ReturnUserProfileViewModel> GetUserProfile(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var address = _addressDto.GetAddressById(user.AddressDeliveryId);

            return new ReturnUserProfileViewModel
            {
                AddressDelivery = address,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<IdentityResult> UpdateUserPassword(JsonPatchDocument<PatchUserViewModel> userPatch, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var updatedUser = new PatchUserViewModel();

                userPatch.ApplyTo(updatedUser);
                return await _userManager.ChangePasswordAsync(user, updatedUser.CurrentPassword, updatedUser.NewPassword);
            }
            return IdentityResult.Failed();
        }
    }
}
