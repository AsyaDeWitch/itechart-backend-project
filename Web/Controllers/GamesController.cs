using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class GamesController : Controller
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("top-platforms")]
        public async Task<IActionResult> GetTopPlatforms()
        {
            Dictionary<string, int> topPlatformsInfo = await _gamesService.GetTopPlatforms(3);

            return Ok(topPlatformsInfo);
        }
    }
}
