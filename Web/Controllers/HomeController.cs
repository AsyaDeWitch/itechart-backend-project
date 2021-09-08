using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("GetInfo")]
        public IActionResult GetInfo()
        {            
            _logger.LogInformation("GetInfo request. Information level");
            return Ok("Hello world!");
        }       
    }
}
