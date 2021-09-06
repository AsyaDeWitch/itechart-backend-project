using BLL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<IdentityUser<int>> _userManager;

        public AdministrationService(RoleManager<IdentityRole<int>> roleManager, UserManager<IdentityUser<int>> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AssignRoleToUser(string email, string roleName)
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

        public async Task<IdentityResult> DeleteUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateRoleAsync(string newRoleName, string oldRoleName)
        {
            if (await _roleManager.RoleExistsAsync(oldRoleName))
            {
                var role = await _roleManager.FindByNameAsync(oldRoleName);
                role.Name = newRoleName;
                return await _roleManager.UpdateAsync(role);
            }
            return IdentityResult.Failed();
        }
    }
}
