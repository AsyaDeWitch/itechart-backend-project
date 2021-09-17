using BLL.Interfaces;
using BLL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using RIL.Models;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ExtendedUser> _userManager;

        public AdministrationService(RoleManager<IdentityRole<int>> roleManager, UserManager<ExtendedUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AssignRoleToUserAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                if (await _roleManager.RoleExistsAsync(roleName))
                {
                    return await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole<int>
                {
                    Name = roleName
                };
                return await _roleManager.CreateAsync(role);
            }
            return IdentityResult.Failed(); ;    
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                return await _roleManager.DeleteAsync(role);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteUserByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateRoleAsync(JsonPatchDocument<PatchUserRoleViewModel> userPatch)
        {
            var updatedRole = new PatchUserRoleViewModel();
            userPatch.ApplyTo(updatedRole);
            if (await _roleManager.RoleExistsAsync(updatedRole.CurrentRole))
            {
                var role = await _roleManager.FindByNameAsync(updatedRole.CurrentRole);
                role.Name = updatedRole.NewRole;
                return await _roleManager.UpdateAsync(role);
            }
            return IdentityResult.Failed();
        }
    }
}
