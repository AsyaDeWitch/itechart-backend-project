using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [Route("")]
        //[Route("Default")]
        [Route("Index")]
        [HttpGet]
        public IActionResult Index()
        {
            //return View();
            return new EmptyResult();
        }

        [HttpGet]
        [Route("GetInfo")]
        public IActionResult GetInfo()
        {
            //return Ok("Hello world!");
            return new OkObjectResult("Hello world!");
        }
        //public string GetInfo()
        //{
        //    //return Ok("Hello world!");
        //    return "Hello world!";
        //}
    }
}
