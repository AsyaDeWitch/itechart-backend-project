using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly UserManager<ExtendedUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService( UserManager<ExtendedUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task BuyOrderAsync(int orderId)
        {
            if(await _unitOfWork.Orders.BuyAsync(orderId))
            {
                var products = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);
                await _unitOfWork.Products.UpdateCountAsync(products);
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
            var order = await _unitOfWork.Orders.CreateAsync(user, totalAmount);

            var productOrders = new List<ProductOrder>();
            foreach (var product in products)
            {
                var productOrder = _mapper.Map<ProductOrder>(product);
                productOrder.Order = order;
                productOrders.Add(productOrder);
            }

            productOrders = await _unitOfWork.ProductOrders.AddProductsToOrderAsync(order.Id, productOrders);

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
            await _unitOfWork.ProductOrders.DeleteProductsFromOrderAsync(orderId, productOrders);

            int totalAmount = 0;
            productOrders = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);
            foreach (var product in productOrders)
            {
                totalAmount += product.ProductAmount;
            }

            await _unitOfWork.Orders.UpdateProductTotalAmountAsync(orderId, totalAmount);
        }

        public async Task<ReturnProductOrderViewModel> GetOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            var products = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);

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
            var orders = await _unitOfWork.Orders.GetListByUserIdAsync(userId);
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
                var address = await _unitOfWork.Addresses.GetByIdAsync(user.AddressDeliveryId);
                order.AddressDelivery = _mapper.Map<AddressViewModel>(address);
            }
            order.Id = orderId;
            var updatedOrder = await _unitOfWork.Orders.UpdateAsync(_mapper.Map<Order>(order));

            if(updatedOrder != null)
            {
                var productOrders = new List<ProductOrder>();
                foreach (var product in products)
                {
                    var productOrder = _mapper.Map<ProductOrder>(product);
                    productOrder.Order = updatedOrder;
                    productOrders.Add(productOrder);
                }
                productOrders = await _unitOfWork.ProductOrders.UpdateProductListInOrderAsync(orderId, productOrders);

                int totalAmount = 0;
                foreach (var product in productOrders)
                {
                    totalAmount += product.ProductAmount;
                }
                await _unitOfWork.Orders.UpdateProductTotalAmountAsync(orderId, totalAmount);

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
                var productOrders = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);
                var productList = new List<ProductOrderViewModel>();
                foreach (var product in productOrders)
                {
                    productList.Add(_mapper.Map<ProductOrderViewModel>(product));
                }
                return new ReturnProductOrderViewModel()
                {
                    ReturnOrderViewModel = _mapper.Map<ReturnOrderViewModel>(await _unitOfWork.Orders.GetByIdAsync(orderId)),
                    ProductOrderViewModels = productList,
                };
            } 
        }
    }
}
