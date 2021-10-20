using BLL.Interfaces;
using BLL.ViewModels;
using DIL.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("orders")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrdersController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        /// <summary>
        /// Performs order creation
        /// </summary>
        /// <param name="products">Product ids with amount need to add to order</param>
        /// <response code="201">Created order returned</response>
        /// <returns>Created order</returns>
        [HttpPost]
        [Authorize(Policy = "RequireUserRole")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReturnProductOrderViewModel))]
        [ServiceFilter(typeof(ProductValidationActionFilter))]
        public async Task<IActionResult> CreateOrderAsync([FromBody]ProductOrderViewModel[] products)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            var userId = int.Parse(_userService.GetUserId(token));

            var createdOrder = await _orderService.CreateOrderAsync(userId, products);
            return Created("/orders/id/" + createdOrder.ReturnOrderViewModel.Id, createdOrder);
        }

        /// <summary>
        /// Performs order list data loading
        /// </summary>
        /// <param name="id">Order id</param>
        /// <response code="200">Order returned</response>
        /// <returns>Order</returns>
        [HttpGet]
        [Authorize(Policy = "RequireUserRole")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnProductOrderViewModel))]
        public async Task<IActionResult> GetOrderAsync(int? id)
        {
            if(id == null)
            {
                return LocalRedirectPermanent("/orders/list");
            }
            var order = await _orderService.GetOrderAsync((int)id);
            return Ok(order);
        }

        /// <summary>
        /// Performs order list products updating
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="orderProducts">Order info need to update and product ids with amount need to add to order or to update in order</param>
        /// <response code="200">Updated order returned</response>
        /// <returns>Updated order</returns>
        [HttpPut]
        [Authorize(Policy = "RequireUserRole")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReturnProductOrderViewModel))]
        [ServiceFilter(typeof(OrderAndProductsValidationActionFilter))]
        public async Task<IActionResult> UpdateOrderAsync(int id, [FromBody] OrderProductsViewModel orderProducts)
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            var userId = int.Parse(_userService.GetUserId(token));
            orderProducts.Order.UserId = userId;
            var updatedOrder = await _orderService.UpdateOrderAsync(id, orderProducts.Order, orderProducts.Products);
            return Ok(updatedOrder);
        }

        /// <summary>
        /// Performs order removing
        /// </summary>
        /// <param name="id">Order id</param>
        /// <param name="products">Product ids with amount need to delete from order</param>
        /// <response code="204">Products from order successfully deleted</response>
        [HttpDelete]
        [Authorize(Policy = "RequireUserRole")]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(ProductValidationActionFilter))]
        public async Task<IActionResult> DeleteProductsFromOrderAsync(int id, [FromBody]ProductOrderViewModel[] products)
        {
            await _orderService.DeleteProductsFromOrderAsync(id,products);
            return NoContent();
        }

        /// <summary>
        /// Performs order creation
        /// </summary>
        /// <response code="204">Order successfully bought</response>
        [HttpPost]
        [Authorize(Policy = "RequireUserRole")]
        [Route("buy")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> BuyOrderAsync(int id)
        {
            await _orderService.BuyOrderAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Performs order list loading
        /// </summary>
        /// <response code="200">Order list returned</response>
        /// <returns>Order list</returns>
        [HttpGet]
        [Authorize(Policy = "RequireUserRole")]
        [Route("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ReturnOrderViewModel>))]
        public async Task<IActionResult> GetOrdersListAsync()
        {
            var token = HttpContext.Request.Cookies["JwtToken"];
            var userId = int.Parse(_userService.GetUserId(token));
            
            var orderList = await _orderService.GetOrdersListAsync(userId);
            return Ok(orderList);
        }
    }
}
