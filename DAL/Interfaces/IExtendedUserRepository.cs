using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IExtendedUserRepository
    {
        public Task<ExtendedUser> FindByIdAsync(string id);
        public Task<ExtendedUser> FindByEmailAsync(string email);
        public Task<IdentityResult> ConfirmEmailAsync(ExtendedUser user, string token);
        public Task<string> GenerateEmailConfirmationTokenAsync(ExtendedUser user);
        public Task<IdentityResult> CreateAsync(ExtendedUser user, string password);
        public Task<ExtendedUser> CreateForSignUpAsync(string email);
        public Task<IdentityResult> UpdateAsync(ExtendedUser user);
        public Task<IdentityResult> DeleteAsync(ExtendedUser user);
        public Task<IdentityResult> ChangePasswordAsync(ExtendedUser user, string currentPassword, string newPassword);
        public Task<IdentityResult> AddToRoleAsync(ExtendedUser user, string role);
        public Task<SignInResult> PasswordSignInAsync(ExtendedUser user, string password);
        public Task<bool> RoleExistsAsync(string roleName);
        public Task<IdentityResult> CreateRoleAsync(string roleName);
        public Task<IdentityResult> UpdateRoleAsync(IdentityRole<int> role);
        public Task<IdentityResult> DeleteRoleAsync(IdentityRole<int> role);
        public Task<IdentityRole<int>> FindRoleByNameAsync(string roleName);
    }
}
