using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdministrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IdentityResult> AssignRoleToUserAsync(string email, string roleName)
        {
            var user = await _unitOfWork.ExtendedUsers.FindByEmailAsync(email);
            if (user == null || !await _unitOfWork.ExtendedUsers.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed();
            }
            return await _unitOfWork.ExtendedUsers.AddToRoleAsync(user, roleName);
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (!await _unitOfWork.ExtendedUsers.RoleExistsAsync(roleName))
            {
                return await _unitOfWork.ExtendedUsers.CreateRoleAsync(roleName);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            if (!await _unitOfWork.ExtendedUsers.RoleExistsAsync(roleName))
            {
                return IdentityResult.Failed();
            }
            var role = await _unitOfWork.ExtendedUsers.FindRoleByNameAsync(roleName);
            return await _unitOfWork.ExtendedUsers.DeleteRoleAsync(role);
        }

        public async Task<IdentityResult> DeleteUserByEmailAsync(string email)
        {
            var user = await _unitOfWork.ExtendedUsers.FindByEmailAsync(email);
            if (user != null)
            {
                return await _unitOfWork.ExtendedUsers.DeleteAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteUserByIdAsync(string id)
        {
            var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(id);
            if (user != null)
            {
                return await _unitOfWork.ExtendedUsers.DeleteAsync(user);
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateRoleAsync(PatchUserRoleViewModel updatedRole)
        {
            if (!await _unitOfWork.ExtendedUsers.RoleExistsAsync(updatedRole.CurrentRole))
            {
                return IdentityResult.Failed();
            }
            var role = await _unitOfWork.ExtendedUsers.FindRoleByNameAsync(updatedRole.CurrentRole);
            role.Name = updatedRole.NewRole;
            return await _unitOfWork.ExtendedUsers.UpdateRoleAsync(role);
        }
    }
}
