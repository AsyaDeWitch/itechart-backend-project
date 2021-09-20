using BLL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public string GetUserId(string token);
        public Task<ReturnUserProfileViewModel> UpdateUserProfile(UserProfileViewModel userProfile, string userId);
        public Task<ReturnUserProfileViewModel> GetUserProfile(string userId);
        public Task<IdentityResult> UpdateUserPassword(JsonPatchDocument<PatchUserViewModel> userPatch, string userId);
    }
}
