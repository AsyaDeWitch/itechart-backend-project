using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    [Route("[controller]")]
    public class AdministrationController : Controller
    {
        private readonly IAdministrationService _administrationService;

        public AdministrationController(IAdministrationService administrationService)
        {
            _administrationService = administrationService;
        }

        [HttpPost]
        [Route("create-role")]
        public async Task<IActionResult> CreateRoleAsync([FromBody] string roleName)
        {
            var result = await _administrationService.CreateRoleAsync(roleName);
            if (result.Succeeded)
            {
                return Created(new Uri("/Auth/sign-in", UriKind.Relative), null);
            }
            return BadRequest("Incorrect role name");
        }

        [HttpDelete]
        [Route("delete-role")]
        public async Task<IActionResult> DeleteRoleAsync([FromBody] string roleName)
        {
            var result = await _administrationService.DeleteRoleAsync(roleName);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name");
        }

        [HttpPut] //convert to patch syntax
        [Route("update-role")]
        public async Task<IActionResult> UpdateRoleAsync([FromBody] string newRoleName, [FromBody] string oldRoleName)
        {
            var result = await _administrationService.UpdateRoleAsync(newRoleName, oldRoleName);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Incorrect role name");
        }

        [HttpDelete]
        [Route("delete-user-by-email")]
        public async Task<IActionResult> DeleteUserByEmailAsync(string email)
        {
            var result = await _administrationService.DeleteUserByEmail(email);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name");
        }

        [HttpDelete]
        [Route("delete-user-by-id")]
        public async Task<IActionResult> DeleteUserByIdAsync(string id)
        {
            var result = await _administrationService.DeleteUserById(id);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Incorrect role name");
        }

        [HttpPost]
        [Route("assign-role-to-user")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] AssignRoleToUserViewModel roleToUserModel)
        {
            var result = await _administrationService.AssignRoleToUser(roleToUserModel.Email, roleToUserModel.RoleName);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest("Incorrect role name");
        }

    }
}
