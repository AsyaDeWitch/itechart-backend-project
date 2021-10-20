using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.ViewModels;
using System.Threading.Tasks;
using BLL.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireUserRole")]
    [Route("user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMemoryCacher _memoryCacher;
        private readonly IConverter _converter;

        public UserController(IUserService userService, IMemoryCacher memoryCacher, IConverter converter)
        {
            _userService = userService;
            _memoryCacher = memoryCacher;
            _converter = converter;
        }

        /// <summary>
        /// Updates profile info
        /// </summary>
        /// <param name="user">User profile info</param>
        /// <response code="200">Updated user profile returned</response>
        /// <response code="400">Invalid phone number</response>
        /// <returns>Updated user profile</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnUserProfileViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserProfileAsync([FromBody] UserProfileViewModel user)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            var userId = _userService.GetUserId(token);
            var updatedUser = await _userService.UpdateUserProfileAsync(user, userId);
            if (updatedUser == null)
            {
                return BadRequest("Existing or invalid name or invalid phone number");
            }
            _memoryCacher.Remove(userId);
            return Ok(updatedUser);
        }

        /// <summary>
        /// Updates user password
        /// </summary>
        /// <param name="userPatch">User old and new passwords</param>
        /// <response code="204">User password updated successfully</response>
        /// <response code="400">Invalid old or new password</response>
        [HttpPatch]
        [Route("password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> UpdateUserPasswordAsync([FromBody]JsonPatchDocument<PatchUserPasswordViewModel> userPatch)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            var userId = _userService.GetUserId(token);
            _memoryCacher.Remove(userId);

            var updatedUser = _converter.User.ApplyTo(userPatch);

            var result = await _userService.UpdateUserPasswordAsync(updatedUser, userId);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Invalid old or new password");
        }

        /// <summary>
        /// Receives user profile
        /// </summary>
        /// <response code="200">Updated user profile returned</response>
        /// <returns>User profile</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnUserProfileViewModel))]
        public async Task<IActionResult> GetUserProfileAsync()
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            var userId = _userService.GetUserId(token);

            if (_memoryCacher.TryGetValue(userId, out var user))
            {
                return Ok(user);
            }

            user = await _userService.GetUserProfileAsync(userId);
            if(user != null)
            {
                _memoryCacher.Set(userId, user);
            }
            return Ok(user);
        }
    }
}
