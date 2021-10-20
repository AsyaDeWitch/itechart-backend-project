using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.ViewModels;
using Microsoft.Extensions.Options;
using DIL.Settings;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    [AllowAnonymous]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly JwtSettings _jwtSettings;

        public AuthController(IAuthService authService, IOptions<JwtSettings> jwtSettings)
        {
            _authService = authService;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Performs user authentication
        /// </summary>
        /// <param name="signInModel">Sign In Model</param>
        /// <response code="200">JWT token string returned</response>
        /// <response code="401">Unsuccessful authentication</response>
        /// <returns>JWT token string</returns>
        [HttpPost]
        [Route("sign-in")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> SignInAsync([FromBody] AuthViewModel signInModel)
        {
            var (user, tokenString) = await _authService.SignInUserAsync(signInModel.Email, signInModel.Password, _jwtSettings.Issuer, _jwtSettings.Audience, _jwtSettings.Key);

            if(user == null || tokenString == null)
            {
                return Unauthorized();
            }
            HttpContext.Response.Cookies.Append("JwtToken", tokenString, new CookieOptions
            {
                MaxAge = TimeSpan.FromMinutes(120)
            });
            return Ok(new { token = tokenString });
        }

        /// <summary>
        /// Performs user creation
        /// </summary>
        /// <param name="signUpModel">Sign Up Model</param>
        /// <response code="201">User created successfully</response>
        /// <response code="400">Email or password is incorrect</response>
        [HttpPost]
        [Route("sign-up")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SignUpAsync([FromBody] AuthViewModel signUpModel)
        {
            var user = await _authService.SignUpUserAsync(signUpModel.Email, signUpModel.Password);

            if(user == null)
            {
                return BadRequest("Email or password is incorrect");
            }
            var token = await _authService.GenerateConfirmationLinkAsync(user);
            var confirmationLink = Url.Action("ConfirmEmail", "Auth", new { user.Id, token }, Request.Scheme);
            await _authService.SendConfirmationLinkAsync(user.Email, confirmationLink);

            return Created(new Uri("/user", UriKind.Relative), null);
        }

        /// <summary>
        /// Performs user email confirmation
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="token">User JWT token</param>
        /// <response code="204">User email confirmed successfully</response>
        /// <response code="400">Confirmation link is incorrect</response>
        [HttpGet]
        [Route("email-confirmation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> ConfirmEmailAsync(string id, string token)
        {
            if (id == null || token == null)
            {
                return BadRequest("Confirmation link is incorrect");
            }
            var result = await _authService.ConfirmEmailAsync(id, token);
            if (result.Succeeded)
            {
                return NoContent();
            }
            return BadRequest("Confirmation link is incorrect");
        }
    }
}
