using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        /// Receives detail occurred exception info and logging them into log file
        /// </summary>
        /// <returns>View with detail occurred exception info </returns>
        [AllowAnonymous]
        [Route("Error")]
        public IActionResult Error()
        {
            // Get the details of the exception that occurred
            var exFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exFeature == null)
            {
                return View();
            }
            // Get which route the exception occurred at
            var path = exFeature.Path;

            // Get the exception that occurred
            var ex = exFeature.Error;

            _logger.LogError(ex, path);

            return View(ex);
        }
    }
}
