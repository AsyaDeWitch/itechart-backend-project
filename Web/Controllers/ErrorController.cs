using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Receives detail occured exception info and loggin them into log file
        /// </summary>
        /// <returns>View with detail occured exception info </returns>
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Get the details of the exception that occurred
            var exFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exFeature != null)
            {
                // Get which route the exception occurred at
                string path = exFeature.Path;

                // Get the exception that occurred
                Exception ex = exFeature.Error;

                _logger.LogError(ex, path);

                return View(ex);
            }
            return View();
        }
    }
}
