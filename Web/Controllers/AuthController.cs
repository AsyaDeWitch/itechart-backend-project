using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("sign-in")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("sign-up")]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("email-confirmation")]
        public IActionResult EmailConfirm()
        {
            return View();
        }
    }
}
