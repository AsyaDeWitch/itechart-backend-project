using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly ITokenService _tokenService;
        private readonly IValidatorService _validatorService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConverter _converter;

        public UserService(ITokenService tokenService, IValidatorService validatorService, IUnitOfWork unitOfWork, IConverter converter)
        {
            _tokenService = tokenService;
            _validatorService = validatorService;
            _unitOfWork = unitOfWork;
            _converter = converter;
        }

        public string GetUserId(string token)
        {
            return _tokenService.ExtractUserIdFromToken(token);
        }

        public async Task<ReturnUserProfileViewModel> UpdateUserProfileAsync(UserProfileViewModel userProfile, string userId)
        {
            var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(userId);
            if (user != null)
            {
                if (!string.IsNullOrWhiteSpace(userProfile.PhoneNumber))
                {
                    if (_validatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                    {
                        user.PhoneNumber = userProfile.PhoneNumber;
                    }
                    else
                        return null; 
                }

                if (!string.IsNullOrWhiteSpace(userProfile.UserName))
                {
                    if(user.UserName != userProfile.UserName)
                    {
                        user.UserName = userProfile.UserName;
                    }
                }

                if (userProfile.AddressDelivery != null)
                {
                    user.AddressDelivery = _converter.Address.ConvertToAddress(userProfile.AddressDelivery);
                }
                else
                {
                    user.AddressDelivery = await _unitOfWork.Addresses.GetByIdAsync(user.AddressDeliveryId);
                }

                var result = await _unitOfWork.ExtendedUsers.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return new ReturnUserProfileViewModel
                    {
                        AddressDelivery = _converter.Address.ConvertToAddressViewModel(user.AddressDelivery),
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
            var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(userId);
            var address = await _unitOfWork.Addresses.GetByIdAsync(user.AddressDeliveryId);

            return new ReturnUserProfileViewModel
            {
                AddressDelivery = _converter.Address.ConvertToAddressViewModel(address),
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
        }

        public async Task<IdentityResult> UpdateUserPasswordAsync(PatchUserPasswordViewModel updatedUser, string userId)
        {
            var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(userId);
            if(user == null || !_validatorService.IsValidPassword(updatedUser.NewPassword))
            {
                return IdentityResult.Failed();
            }
            return await _unitOfWork.ExtendedUsers.ChangePasswordAsync(user, updatedUser.CurrentPassword, updatedUser.NewPassword);
        }
    }
}
