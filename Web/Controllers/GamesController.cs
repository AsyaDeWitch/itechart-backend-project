using BLL.Interfaces;
using BLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("games")]
    public class GamesController : Controller
    {
        private readonly IGamesService _gamesService;

        public GamesController(IGamesService gamesService)
        {
            _gamesService = gamesService;
        }

        /// <summary>
        /// Performs main page data loading
        /// </summary>
        /// <response code="200">Top 3 platforms info returned</response>
        /// <returns>Top 3 platforms info </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("top-platforms")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dictionary<string, int>))]
        public async Task<IActionResult> GetTopPlatforms()
        {
            Dictionary<string, int> topPlatformsInfo = await _gamesService.GetTopPlatformsAsync(3);

            return Ok(topPlatformsInfo);
        }

        /// <summary>
        /// Receives data matches search term from search field
        /// </summary>
        /// <param name="term">Minimum release date for the game</param>
        /// <param name="limit">Count of games need to receive</param>
        /// <param name="offset">Minimum game score on a 10-point scale</param>
        /// <param name="name">Game name or part of the game name</param>
        /// /// <response code="200">Games mathes search term returned</response>
        /// <returns>Games mathes search term</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("search")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductViewModel>))]
        public async Task<IActionResult> SearchGamesByParameters(DateTime term, int limit, double offset, string name)
        {
            var products = await _gamesService.SearchGamesByParametersAsync(term, limit, offset, name);

            return Ok(products);
        }
    }
}
