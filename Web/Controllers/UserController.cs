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

namespace Web.Controllers
{
    [Authorize(Policy = "RequireUserRole")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut]
        [Route("user")]
        public IActionResult UpdateUserProfile([FromBody] UserProfileViewModel user)
        {
            return Ok();
        }

        public IActionResult UpdateUserPassword()
        {
            return NoContent();
        }

        [HttpGet]
        [Route("user")]
        public IActionResult GetUserProfile()
        {
            if (HttpContext.Request.Cookies.ContainsKey("JwtToken"))
            {
                //validate token
                string token = HttpContext.Request.Cookies["JwtToken"];
                //ClaimsPrincipal principal = g 
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                var id = jwtToken.Claims.First(claim => claim.Type == "UserId").Value;
                return Ok();
            }
            return Unauthorized();
        }
    }
}
