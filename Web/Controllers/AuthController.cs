using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;

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
        public async Task<IActionResult> SignIn([FromBody] AuthModel signInModel)
        {
            var user = await _authService.SignInUserAsync(signInModel.Email, signInModel.Password);

            if(user != null)
            {
                var tokenString = _authService.GenerateJwt(user);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] AuthModel signUpModel)
        {
            var user = await _authService.SignUpUserAsync(signUpModel.Email, signUpModel.Password);

            if(user != null)
            {
                return Created(new Uri("/Auth/email-confirmation", UriKind.Relative), null);
            }

            return BadRequest("Email or password is incorrect");
        }

        [HttpGet]
        [Route("email-confirmation")]
        public IActionResult ConfirmEmail()
        {
            return View();
        }
    }
}
