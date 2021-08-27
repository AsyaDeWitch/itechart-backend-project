using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return new EmptyResult();
        }

        [HttpGet]
        [Route("GetInfo")]
        public IActionResult GetInfo()
        {
            return new OkObjectResult("Hello world!");
        }
    }
}
