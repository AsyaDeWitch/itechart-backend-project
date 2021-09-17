using BLL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdministrationService
    {
        public Task<IdentityResult> CreateRoleAsync(string roleName);
        public Task<IdentityResult> DeleteRoleAsync(string roleName);
        public Task<IdentityResult> UpdateRoleAsync(JsonPatchDocument<PatchUserRoleViewModel> userPatch);
        public Task<IdentityResult> DeleteUserByEmailAsync(string email);
        public Task<IdentityResult> DeleteUserByIdAsync(string id);
        public Task<IdentityResult> AssignRoleToUserAsync(string email, string roleName);
    }
}
