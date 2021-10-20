using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Test action for Admin role policy, logging and response body, contains string "Hello World"
        /// </summary>
        /// <response code="200">String "Hello world" returned</response>
        /// <returns>"Hello world" string</returns>
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("get-info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public IActionResult GetInfo()
        {            
            _logger.LogInformation("GetInfo request. Information level");
            return Ok("Hello world!");
        }       
    }
}
