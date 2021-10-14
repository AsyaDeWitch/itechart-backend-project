using BLL.Interfaces;
using BLL.ViewModels;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConverter _converter;

        public OrderService(IUnitOfWork unitOfWork, IConverter converter)
        {
            _unitOfWork = unitOfWork;
            _converter = converter;
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
            var totalAmount = products
                .Sum(p => p.ProductAmount);
            var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(userId.ToString());
            var order = await _unitOfWork.Orders.CreateAsync(user, totalAmount);

            var productOrders = _converter.ProductOrder.ConvertToProductOrderList(products, order);

            productOrders = await _unitOfWork.ProductOrders.AddProductsToOrderAsync(order.Id, productOrders);

            var productList = _converter.ProductOrder.ConvertToProductOrderViewModelList(productOrders);

            return new ReturnProductOrderViewModel()
            {
                ReturnOrderViewModel = _converter.Order.ConvertToReturnOrderViewModel(order),
                ProductOrderViewModels = productList,
            };
        }

        public async Task DeleteProductsFromOrderAsync(int orderId, ProductOrderViewModel[] products)
        {
            var productOrders = _converter.ProductOrder.ConvertToProductOrderList(products);
            await _unitOfWork.ProductOrders.DeleteProductsFromOrderAsync(orderId, productOrders);

            productOrders = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);
            var totalAmount = productOrders
                .Sum(p => p.ProductAmount);

            await _unitOfWork.Orders.UpdateProductTotalAmountAsync(orderId, totalAmount);
        }

        public async Task<ReturnProductOrderViewModel> GetOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            var products = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);

            var productList = _converter.ProductOrder.ConvertToProductOrderViewModelList(products);

            return new ReturnProductOrderViewModel()
            {
                ReturnOrderViewModel = _converter.Order.ConvertToReturnOrderViewModel(order),
                ProductOrderViewModels = productList,
            };
        }

        public async Task<List<ReturnOrderViewModel>> GetOrdersListAsync(int userId)
        {
            var orders = await _unitOfWork.Orders.GetListByUserIdAsync(userId);
            var orderList = new List<ReturnOrderViewModel>();
            orderList.AddRange
                (orders
                    .Select(o => _converter.Order.ConvertToReturnOrderViewModel(o)));
            return orderList;
        }

        public async Task<ReturnProductOrderViewModel> UpdateOrderAsync(int orderId, OrderViewModel order, ProductOrderViewModel[] products)
        {
            if (order.AddressDelivery == null)
            {
                var user = await _unitOfWork.ExtendedUsers.FindByIdAsync(order.UserId.ToString());
                var address = await _unitOfWork.Addresses.GetByIdAsync(user.AddressDeliveryId);
                order.AddressDelivery = _converter.Address.ConvertToAddressViewModel(address);
            }
            order.Id = orderId;
            var updatedOrder = await _unitOfWork.Orders.UpdateAsync(_converter.Order.ConvertToOrder(order));

            if(updatedOrder != null)
            {
                var productOrders = _converter.ProductOrder.ConvertToProductOrderList(products, updatedOrder);

                productOrders = await _unitOfWork.ProductOrders.UpdateProductListInOrderAsync(orderId, productOrders);
                var totalAmount = productOrders
                    .Sum(p => p.ProductAmount);

                await _unitOfWork.Orders.UpdateProductTotalAmountAsync(orderId, totalAmount);

                var productList = new List<ProductOrderViewModel>();
                productList.AddRange
                (productOrders
                    .Select(p => _converter.ProductOrder.ConvertToProductOrderViewModel(p)));

                return new ReturnProductOrderViewModel()
                {
                    ReturnOrderViewModel = _converter.Order.ConvertToReturnOrderViewModel(updatedOrder),
                    ProductOrderViewModels = productList,
                };
            }
            else
            {
                var productOrders = await _unitOfWork.ProductOrders.GetProductListByOrderIdAsync(orderId);
                var productList = new List<ProductOrderViewModel>();
                productList.AddRange
                (productOrders
                    .Select(p => _converter.ProductOrder.ConvertToProductOrderViewModel(p)));

                return new ReturnProductOrderViewModel()
                {
                    ReturnOrderViewModel = _converter.Order.ConvertToReturnOrderViewModel(await _unitOfWork.Orders.GetByIdAsync(orderId)),
                    ProductOrderViewModels = productList,
                };
            } 
        }

       
    }
}
