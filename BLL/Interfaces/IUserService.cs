using BLL.ViewModels;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public string GetUserId(string token);
        public Task<ReturnUserProfileViewModel> UpdateUserProfile(UserProfileViewModel userProfile, string userId);
        public Task<ReturnUserProfileViewModel> GetUserProfile(string userId);
    }
}
