using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Data;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly OrderDto _orderDto;
        private readonly ProductOrderDto _productOrderDto;
        private readonly AddressDto _addressDto;
        private readonly ProductDto _productDto;

        public OrderService( UserManager<ExtendedUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _orderDto = new OrderDto(_context);
            _productOrderDto = new ProductOrderDto(_context);
            _addressDto = new AddressDto(_context);
            _productDto = new ProductDto(_context);
            _mapper = mapper;
        }

        public async Task BuyOrderAsync(int orderId)
        {
            if(await _orderDto.BuyOrderAsync(orderId))
            {
                var products = await _productOrderDto.GetOrderProductListAsync(orderId);
                await _productDto.UpdateProductCountAsync(products);
            }
        }

        public async Task<ReturnProductOrderViewModel> CreateOrderAsync(int userId, ProductOrderViewModel[] products)
        {
            int totalAmount = 0;
            foreach(var product in products)
            {
                totalAmount += product.ProductAmount;
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            var order = await _orderDto.CreateOrderAsync(user, totalAmount);

            var productOrders = new List<ProductOrder>();
            foreach (var product in products)
            {
                var productOrder = _mapper.Map<ProductOrder>(product);
                productOrder.Order = order;
                productOrders.Add(productOrder);
            }

            productOrders = await _productOrderDto.AddProductsToOrderAsync(order.Id, productOrders);

            var productList = new List<ProductOrderViewModel>();
            foreach (var product in productOrders)
            {
                productList.Add(_mapper.Map<ProductOrderViewModel>(product));
            }

            return new ReturnProductOrderViewModel()
            {
                ReturnOrderViewModel = _mapper.Map<ReturnOrderViewModel>(order),
                ProductOrderViewModels = productList,
            };
        }

        public async Task DeleteProductsFromOrderAsync(int orderId, ProductOrderViewModel[] products)
        {
            var productOrders = new List<ProductOrder>();
            foreach (var product in products)
            {
                productOrders.Add(_mapper.Map<ProductOrder>(product));
            }
            await _productOrderDto.DeleteProductsFromOrderAsync(orderId, productOrders);

            int totalAmount = 0;
            productOrders = await _productOrderDto.GetOrderProductListAsync(orderId);
            foreach (var product in productOrders)
            {
                totalAmount += product.ProductAmount;
            }

            await _orderDto.UpdateProductTotalAmount(orderId, totalAmount);
        }

        public async Task<ReturnProductOrderViewModel> GetOrderAsync(int ordertId)
        {
            var order = await _orderDto.GetOrderInfoAsync(ordertId);
            var products = await _productOrderDto.GetOrderProductListAsync(ordertId);

            var productList = new List<ProductOrderViewModel>();
            foreach (var product in products)
            {
                productList.Add(_mapper.Map<ProductOrderViewModel>(product));
            }

            return new ReturnProductOrderViewModel()
            {
                ReturnOrderViewModel = _mapper.Map<ReturnOrderViewModel>(order),
                ProductOrderViewModels = productList,
            };
        }

        public async Task<List<ReturnOrderViewModel>> GetOrdersListAsync(int userId)
        {
            var orders = await _orderDto.GetOrdersListAsync(userId);
            var orderList = new List<ReturnOrderViewModel>();
            foreach (var order in orders)
            {
                orderList.Add(_mapper.Map<ReturnOrderViewModel>(order));
            }

            return orderList;
        }

        public async Task<ReturnProductOrderViewModel> UpdateOrderAsync(int orderId, OrderViewModel order, ProductOrderViewModel[] products)
        {
            if (order.AddressDelivery == null)
            {
                var user = await _userManager.FindByIdAsync(order.UserId.ToString());
                var address = await _addressDto.GetAddressById(user.AddressDeliveryId);
                order.AddressDelivery = _mapper.Map<AddressViewModel>(address);
            }
            var updatedOrder = await _orderDto.UpdateOrderInfoAsync(orderId, _mapper.Map<Order>(order));

            if(updatedOrder != null)
            {
                var productOrders = new List<ProductOrder>();
                foreach (var product in products)
                {
                    var productOrder = _mapper.Map<ProductOrder>(product);
                    productOrder.Order = updatedOrder;
                    productOrders.Add(productOrder);
                }
                productOrders = await _productOrderDto.UpdateOrderProductListAsync(orderId, productOrders);

                int totalAmount = 0;
                foreach (var product in productOrders)
                {
                    totalAmount += product.ProductAmount;
                }
                await _orderDto.UpdateProductTotalAmount(orderId, totalAmount);

                var productList = new List<ProductOrderViewModel>();
                foreach (var product in productOrders)
                {
                    productList.Add(_mapper.Map<ProductOrderViewModel>(product));
                }

                return new ReturnProductOrderViewModel()
                {
                    ReturnOrderViewModel = _mapper.Map<ReturnOrderViewModel>(updatedOrder),
                    ProductOrderViewModels = productList,
                };
            }
            else
            {
                var productOrders = await _productOrderDto.GetOrderProductListAsync(orderId);
                var productList = new List<ProductOrderViewModel>();
                foreach (var product in productOrders)
                {
                    productList.Add(_mapper.Map<ProductOrderViewModel>(product));
                }
                return new ReturnProductOrderViewModel()
                {
                    ReturnOrderViewModel = _mapper.Map<ReturnOrderViewModel>(await _orderDto.GetOrderInfoAsync(orderId)),
                    ProductOrderViewModels = productList,
                };
            } 
        }
    }
}
