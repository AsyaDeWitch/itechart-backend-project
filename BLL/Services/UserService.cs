using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Interfaces;
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
        private readonly IMapper _mapper;
        private readonly IValidatorService _validatorService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(ITokenService tokenService, UserManager<ExtendedUser> userManager, IMapper mapper, IValidatorService validatorService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _validatorService = validatorService;
            _unitOfWork = unitOfWork;
        }

        public string GetUserId(string token)
        {
            return _tokenService.ExtractUserIdFromToken(token);
        }

        public async Task<ReturnUserProfileViewModel> UpdateUserProfileAsync(UserProfileViewModel userProfile, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (!String.IsNullOrWhiteSpace(userProfile.PhoneNumber))
                {
                    if (_validatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                    {
                        user.PhoneNumber = userProfile.PhoneNumber;
                    }
                    else
                        return null; 
                }

                if (!String.IsNullOrWhiteSpace(userProfile.UserName))
                {
                    if(user.UserName != userProfile.UserName)
                    {
                        user.UserName = userProfile.UserName;
                    }
                }

                if (userProfile.AddressDelivery != null)
                {
                    user.AddressDelivery = _mapper.Map<Address>(userProfile.AddressDelivery);
                }
                else
                {
                    user.AddressDelivery = await _unitOfWork.Addresses.GetByIdAsync(user.AddressDeliveryId);
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ReturnUserProfileViewModel
                    {
                        AddressDelivery = _mapper.Map<AddressViewModel>(user.AddressDelivery),
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                    };
                }
            }
            return null;
        }

        public async Task<ReturnUserProfileViewModel> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var address = await _unitOfWork.Addresses.GetByIdAsync(user.AddressDeliveryId);

            return new ReturnUserProfileViewModel
            {
                AddressDelivery = _mapper.Map <AddressViewModel>(address),
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<IdentityResult> UpdateUserPasswordAsync(JsonPatchDocument<PatchUserPasswordViewModel> userPatch, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var updatedUser = new PatchUserPasswordViewModel();

                userPatch.ApplyTo(updatedUser);
                if (_validatorService.IsValidPassword(updatedUser.NewPassword))
                {
                    return await _userManager.ChangePasswordAsync(user, updatedUser.CurrentPassword, updatedUser.NewPassword);
                } 
            }
            return IdentityResult.Failed();
        }
    }
}
