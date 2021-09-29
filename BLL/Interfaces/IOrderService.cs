using BLL.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderService
    {
        public Task<ReturnProductOrderViewModel> CreateOrderAsync(int userId, ProductOrderViewModel[] products);
        public Task<ReturnProductOrderViewModel> GetOrderAsync(int ordertId);
        public Task<ReturnProductOrderViewModel> UpdateOrderAsync(int orderId, OrderViewModel order,ProductOrderViewModel[] products);
        public Task DeleteProductsFromOrderAsync(int orderId, ProductOrderViewModel[] products);
        public Task BuyOrderAsync(int orderId);
        public Task<List<ReturnOrderViewModel>> GetOrdersListAsync(int userId);
    }
}
