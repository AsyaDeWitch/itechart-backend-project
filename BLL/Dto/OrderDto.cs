using DAL.Data;
using DAL.Repositories;
using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class OrderDto
    {

        private readonly ApplicationDbContext _context;
        private readonly OrderRepository _orderRepository;

        public OrderDto(ApplicationDbContext context)
        {
            _context = context;
            _orderRepository = new OrderRepository(_context);
        }

        public async Task<bool> BuyOrderAsync(int orderId)
        {
            return await _orderRepository.BuyOrderAsync(orderId);
        }

        public async Task<Order> GetOrderInfoAsync(int orderId)
        {
            return await _orderRepository.GetOrderInfoAsync(orderId);
        }

        public async Task<Order> CreateOrderAsync(ExtendedUser user, int totalAmount)
        {
            return await _orderRepository.CreateOrderAsync(user, totalAmount);
        }

        public async Task<List<Order>> GetOrdersListAsync(int userId)
        {
            return await _orderRepository.GetOrdersListAsync(userId);
        }

        public async Task<Order> UpdateOrderInfoAsync(int orderId, Order order)
        {
            return await _orderRepository.UpdateOrderInfoAsync(orderId, order);
        }

        public async Task<Order> UpdateProductTotalAmount(int orderId, int totalAmount)
        {
            return await _orderRepository.UpdateProductTotalAmount(orderId, totalAmount);
        }
    }
}
