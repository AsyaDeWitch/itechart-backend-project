using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace Web.Controllers
{
    [AllowAnonymous]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtAuthService _authService;

        public AuthController(IJwtAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("sign-in")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            var user = await _authService.SignInUserAsync(email, password);

            if(user != null)
            {
                var tokenString = _authService.GenerateJwt(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp(string email, string password)
        {
            var user = await _authService.SignUpUserAsync(email, password);

            if(user != null)
            {
                return Created(new Uri("/Auth/sign-in", UriKind.Relative), null);
            }

            return BadRequest("Email or password is incorrect");
        }

        [HttpGet]
        [Route("email-confirmation")]
        public IActionResult EmailConfirm()
        {
            return View();
        }
    }
}
