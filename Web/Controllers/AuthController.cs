using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> SignInAsync([FromBody] AuthModel signInModel)
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
        public async Task<IActionResult> SignUpAsync([FromBody] AuthModel signUpModel)
        {
            var user = await _authService.SignUpUserAsync(signUpModel.Email, signUpModel.Password);

            if(user != null)
            {
                var token = await _authService.GenerateComfirmationLinkAsync(user);
                var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { user.Id, token }, Request.Scheme);
                await _authService.SendConfirmationLinkAsync(user.Email, confirmationLink);

                return Created(new Uri("/Auth/sign-in", UriKind.Relative), null);
            }

            return BadRequest("Email or password is incorrect");
        }

        [HttpGet]
        [Route("email-confirmation")]
        public async Task<IActionResult> ConfirmEmailAsync(string id, string token)
        {
            if (id != null && token != null)
            {
                var result = await _authService.ConfirmEmailAsync(id, token);
                if(result.Succeeded)
                {
                    return NoContent();
                }
            }
            return BadRequest("Confirmation link is incorrect");
        }
    }
}
