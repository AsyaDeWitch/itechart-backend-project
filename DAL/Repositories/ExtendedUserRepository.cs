using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ExtendedUserRepository : IExtendedUserRepository
    {
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly SignInManager<ExtendedUser> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public ExtendedUserRepository(RoleManager<IdentityRole<int>> roleManager, UserManager<ExtendedUser> userManager, SignInManager<ExtendedUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(ExtendedUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ExtendedUser user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ExtendedUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityResult> CreateAsync(ExtendedUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public ExtendedUser CreateForSignUp(string email)
        {
            return new ExtendedUser
            {
                UserName = email,
                Email = email,
            };
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole<int>
            {
                Name = roleName
            };
            return await _roleManager.CreateAsync(role);
        }

        public async Task<IdentityResult> DeleteAsync(ExtendedUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IdentityResult> DeleteRoleAsync(IdentityRole<int> role)
        {
            return await _roleManager.DeleteAsync(role);
        }

        public async Task<ExtendedUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ExtendedUser> FindByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityRole<int>> FindRoleByNameAsync(string roleName)
        {
            return await _roleManager.FindByNameAsync(roleName);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ExtendedUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<SignInResult> PasswordSignInAsync(ExtendedUser user, string password)
        {
            return await _signInManager.PasswordSignInAsync(user, password, false, false);
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<IdentityResult> UpdateAsync(ExtendedUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole<int> role)
        {
            return await _roleManager.UpdateAsync(role);
        }
    }
}
