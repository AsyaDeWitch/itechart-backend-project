using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BuyAsync(int id)
        {
            var order = await _context.Orders
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();

            if (order != null && order.Status != (int)OrderStatus.Completed)
            {
                order.Status = (int)OrderStatus.Completed;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Order> CreateAsync(ExtendedUser user, int totalAmount)
        {
            var order = new Order()
            {
                CreationDate = DateTime.Now,
                TotalAmount = totalAmount,
                DeliveryType = (int)DeliveryType.On_Demand_Delivery,
                AddressDelivery = user.AddressDelivery,
                User = user,
            };

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetListByUserIdAsync(int userId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<Order> UpdateAsync(Order newOrder)
        {
            var order = await _context.Orders
               .Where(o => o.Id == newOrder.Id && o.Status == (int)OrderStatus.Awaiting_Payment)
               .FirstOrDefaultAsync();

            if (order != null)
            {
                order.DeliveryType = newOrder.DeliveryType;
                if(newOrder.AddressDelivery != null)
                {
                    order.AddressDelivery = newOrder.AddressDelivery;
                }
                await _context.SaveChangesAsync();
            }
            return order;
        }

        public async Task<Order> UpdateProductTotalAmountAsync(int id, int totalAmount)
        {
            var oldOrder = await _context.Orders
               .Where(o => o.Id == id && o.Status == (int)OrderStatus.Awaiting_Payment)
               .FirstOrDefaultAsync();

            if (oldOrder != null)
            {
                oldOrder.TotalAmount = totalAmount;
                await _context.SaveChangesAsync();
            }
            return oldOrder;
        }
    }
}
