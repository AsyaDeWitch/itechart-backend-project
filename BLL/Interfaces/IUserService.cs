using BLL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserService
    {
        public string GetUserId(string token);
        public Task<ReturnUserProfileViewModel> UpdateUserProfileAsync(UserProfileViewModel userProfile, string userId);
        public Task<ReturnUserProfileViewModel> GetUserProfileAsync(string userId);
        public Task<IdentityResult> UpdateUserPasswordAsync(JsonPatchDocument<PatchUserPasswordViewModel> userPatch, string userId);
    }
}
