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

        [HttpGet]
        [AllowAnonymous]
        [Route("search")]
        public async Task<IActionResult> SearchGamesByParameters(DateTime? term, int? limit, double? offset, string name)
        {
            if (term == null && limit == null && offset == null)
            {
                var products = await _gamesService.SearchGamesByName(name);
                return Ok(products);
            }

            return Ok();
        }
    }
}
