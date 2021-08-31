﻿using Microsoft.AspNetCore.Mvc;
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
            _logger.LogInformation("GetInfo request. Information level");
            return new OkObjectResult("Hello world!");
        }
    }
}
