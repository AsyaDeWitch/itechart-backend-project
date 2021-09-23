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
        private readonly IUserService _userService;

        public GamesController(IGamesService gamesService, IUserService userService)
        {
            _gamesService = gamesService;
            _userService = userService;
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
        /// <response code="200">Games mathes search term returned</response>
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

        /// <summary>
        /// Performs product description
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <response code="200">Product full description returned</response>
        /// <returns>Full info about product</returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        public async Task<IActionResult> GetProductFullInfoAsync(string id)
        {
            var product = await _gamesService.GetProductFullInfoAsync(id);

            return Ok(product);
        }

        /// <summary>
        /// Performs product creation
        /// </summary>
        /// <param name="product">Full info about product + attached images for logo and background</param>
        /// <response code="201">Created product full description returned</response>
        /// <returns>Created product</returns>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductViewModel))]
        public async Task<IActionResult> CreateProductAsync([FromForm]ProductViewModel product)
        {
            var createdProduct = await _gamesService.CreateProductAsync(product);
            return Created("/games/id/" + createdProduct.Id.ToString(), createdProduct);
        }

        /// <summary>
        /// Performs product editing
        /// </summary>
        /// <param name="updatedProduct">Full info about product</param>
        /// <response code="200">Updated product full description returned</response>
        /// <returns>Updated product</returns>
        [HttpPut]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        public async Task<IActionResult> UpdateProductAsync([FromForm]ProductViewModel updatedProduct)
        {
            var product = await _gamesService.UpdateProductAsync(updatedProduct);
            return Ok(product);
        }

        /// <summary>
        /// Performs product removing
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <response code="204">Product removed</response>
        [HttpDelete]
        [Authorize(Policy = "RequireAdminRole")]
        [Route("id/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProductByIdAsync(string id)
        {
            await _gamesService.DeleteProductByIdAsync(id);

            return NoContent();
        }

        /// <summary>
        /// Performs product rating creation
        /// </summary>
        /// <param name="productRating">Product rating info</param>
        /// <response code="201">Created product rating returned</response>
        /// <returns>Created product rating</returns>
        [HttpPost]
        [Authorize(Policy = "RequireUserRole")]
        [Route("rating")]
        [ProducesResponseType(StatusCodes.Status201Created /*PRVM*/)]
        public async Task<IActionResult> CreateProductRatingAsync([FromBody]ProductRatingViewModel productRating)
        {
            string token = HttpContext.Request.Cookies["JwtToken"];
            productRating.UserId = int.Parse(_userService.GetUserId(token));
            var createdProductRating = await _gamesService.CreateProductRatingAsync(productRating);
            return Created("/games/rating/", createdProductRating);
        }
    }
}
