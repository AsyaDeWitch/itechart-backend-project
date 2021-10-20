using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("administration")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService _administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        /// <summary>
        /// Performs role creation
        /// </summary>
        /// <param name="roleName">Name of new role</param>
        /// <response code="201">Role created successfully</response>
        /// <response code="400">Incorrect role name</response>
        [HttpPost]
        [Route("create-role")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> CreateRoleAsync([FromBody] string roleName)
        {
            var result = await _administrationService.CreateRoleAsync(roleName);
            if (result.Succeeded)
            {
                return Created(new Uri("/user", UriKind.Relative), null);
            }
            return BadRequest("Incorrect role name");
        }

        /// <summary>
        /// Performs role deletion
        /// </summary>
        /// <param name="roleName">Name of role need to delete</param>
        /// <response code="204">Role deleted successfully</response>
        /// <response code="400">Incorrect role name</response>
        [HttpDelete]
        [Route("delete-role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteRoleAsync([FromBody] string roleName)
        {
            var result = await _administrationService.DeleteRoleAsync(roleName);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name");
        }

        /// <summary>
        /// Performs role renewal
        /// </summary>
        /// <param name="userPatch">User old and new roles</param>
        /// <response code="204">Role updated successfully</response>
        /// <response code="400">Incorrect role name(s)</response>
        [HttpPatch]
        [Route("update-role")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserRoleAsync([FromBody] JsonPatchDocument<PatchUserRoleViewModel> userPatch)
        {
            var updatedRole = new PatchUserRoleViewModel();
            userPatch.ApplyTo(updatedRole);
            var result = await _administrationService.UpdateRoleAsync(updatedRole);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name(s)");
        }

        /// <summary>
        /// Performs user deletion by email
        /// </summary>
        /// <param name="email">Email of user need to delete</param>
        /// /// <response code="204">User deleted successfully</response>
        /// <response code="400">Incorrect role name or user email</response>
        [HttpDelete]
        [Route("delete-user-by-email")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteUserByEmailAsync(string email)
        {
            var result = await _administrationService.DeleteUserByEmailAsync(email);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name or user email");
        }

        /// <summary>
        /// Performes user deletion by id
        /// </summary>
        /// <param name="id">Id of user need to delete</param>
        /// <response code="204">User deleted successfully</response>
        /// <response code="400">Incorrect role name or user id</response>
        [HttpDelete]
        [Route("delete-user-by-id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> DeleteUserByIdAsync(string id)
        {
            var result = await _administrationService.DeleteUserByIdAsync(id);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect user id");
        }

        /// <summary>
        /// Performs assigning role to user
        /// </summary>
        /// <response code="204">Role assigned to user successfully</response>
        /// <response code="400">Incorrect role name or user email</response>
        [HttpPost]
        [Route("assign-role-to-user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> AssignRoleToUserAsync([FromBody] AssignRoleToUserViewModel roleToUserModel)
        {
            var result = await _administrationService.AssignRoleToUserAsync(roleToUserModel.Email, roleToUserModel.RoleName);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name or user email");
        }
    }
}
