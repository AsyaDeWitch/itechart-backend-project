using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAdministrationService
    {
        public Task<IdentityResult> CreateRoleAsync(string roleName);
        public Task<IdentityResult> DeleteRoleAsync(string roleName);
        public Task<IdentityResult> UpdateRoleAsync(string newRoleName, string oldRoleName);
        public Task<IdentityResult> DeleteUserByEmail(string email);
        public Task<IdentityResult> DeleteUserById(string id);
        public Task<IdentityResult> AssignRoleToUser(string email, string roleName);
    }
}
