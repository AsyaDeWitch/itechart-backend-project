using BLL.Dto;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (ValidatorService.IsValidPhoneNumber(userProfile.PhoneNumber))
                {
                    if (userProfile.UserName != null)
                    {
                        user.UserName = userProfile.UserName;
                    }

                    if (userProfile.PhoneNumber != null)
                    {
                        user.PhoneNumber = userProfile.PhoneNumber;
                    }
                    
                    if(userProfile.AddressDelivery != null)
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
    }
}
