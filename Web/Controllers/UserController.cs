using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using DIL.Settings;
using Microsoft.AspNetCore.JsonPatch;

namespace Web.Controllers
{
    [Authorize(Policy = "RequireUserRole")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly JwtSettings _jwtSettings;

        public UserController(IUserService userService, IOptions<JwtSettings> jwtSettings)
        {
            _userService = userService;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPut]
        [Route("user")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileViewModel user)
        {
            if (HttpContext.Request.Cookies.ContainsKey("JwtToken"))
            {
                string token = HttpContext.Request.Cookies["JwtToken"];
                var userId = _userService.GetUserId(token);
                var updatedUser = await _userService.UpdateUserProfile(user, userId);
                if (updatedUser != null)
                {
                    return Ok(updatedUser);
                }
                return BadRequest("Invalid phone number");
            }
            return Unauthorized();
        }

        [HttpPatch]
        [Route("user/password")]
        public async Task<IActionResult> UpdateUserPassword([FromBody]JsonPatchDocument<PatchUserViewModel> userPatch)
        {
            if (HttpContext.Request.Cookies.ContainsKey("JwtToken"))
            {
                string token = HttpContext.Request.Cookies["JwtToken"];
                var userId = _userService.GetUserId(token);
                var result = await _userService.UpdateUserPassword(userPatch, userId);
                if (result.Succeeded)
                {
                    return NoContent();
                }
            }
            return Unauthorized();
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUserProfile()
        {
            if (HttpContext.Request.Cookies.ContainsKey("JwtToken"))
            {
                string token = HttpContext.Request.Cookies["JwtToken"];
                var userId = _userService.GetUserId(token);
                var user = await _userService.GetUserProfile(userId);
                return Ok(user);
            }
            return Unauthorized();
        }
    }
}
