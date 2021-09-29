using DAL.Data;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BuyOrderAsync(int orderId)
        {
            var order = await _context.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();

            if (order != null && order.Status != (int)OrderStatus.Completed)
            {
                order.Status = (int)OrderStatus.Completed;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Order> GetOrderInfoAsync(int orderId)
        {
            return await _context.Orders
                .Where(o => o.Id == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> CreateOrderAsync(ExtendedUser user, int totalAmount)
        {
            var order = new Order()
            {
                CreationDate = DateTime.Now,
                TotalAmount = totalAmount,
                DeliveryType = (int)DeliveryType.On_Demand_Delivery,
                AddressDelivery = user.AddressDelivery,
                User = user,
            };

            await _context.AddAsync<Order>(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetOrdersListAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> UpdateOrderInfoAsync(int orderId, Order order)
        {
            var oldOrder = await _context.Orders
               .Where(o => o.Id == orderId && o.Status == (int)OrderStatus.Awaiting_Payment)
               .FirstOrDefaultAsync();
            if (oldOrder != null)
            {
                oldOrder.DeliveryType = order.DeliveryType;
                if(order.AddressDelivery != null)
                {
                    oldOrder.AddressDelivery = order.AddressDelivery;
                }
                await _context.SaveChangesAsync();
            }
            return oldOrder;
        }

        public async Task<Order> UpdateProductTotalAmount(int orderId, int totalAmount)
        {
            var oldOrder = _context.Orders
               .Where(o => o.Id == orderId && o.Status == (int)OrderStatus.Awaiting_Payment)
               .FirstOrDefault();
            if (oldOrder != null)
            {
                oldOrder.TotalAmount = totalAmount;
                _context.Update<Order>(oldOrder);
                await _context.SaveChangesAsync();
            }
            return oldOrder;
        }
    }
}
