using DAL.Data;
using RIL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductOrder>> GetOrderProductListAsync(int orderId)
        {
            return await _context.ProductOrders
                .AsNoTracking()
                .Where(po => po.OrderId == orderId)
                .ToListAsync();
        }

        public async Task DeleteProductsFromOrderAsync(int orderId, List<ProductOrder> products)
        {
            foreach(var product in products)
            {
                var oldProduct = await _context.ProductOrders
                   .Where(po => po.OrderId == orderId && po.ProductId == product.ProductId)
                   .FirstOrDefaultAsync();
                if (oldProduct != null)
                {
                    _context.Remove<ProductOrder>(oldProduct);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<ProductOrder>> AddProductsToOrderAsync(int orderId, List<ProductOrder> products)
        {
            await _context.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            return await _context.ProductOrders
                .AsNoTracking()
                .Where(po => po.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<List<ProductOrder>> UpdateOrderProductListAsync(int orderId, List<ProductOrder> products)
        {
            foreach(var product in products)
            {
                var oldProduct = await _context.ProductOrders
                    .Where(po => po.OrderId == orderId && po.ProductId == product.ProductId)
                    .FirstOrDefaultAsync();
                if (oldProduct != null)
                {
                    oldProduct.ProductAmount = product.ProductAmount;
                }
                else
                {
                    await _context.AddAsync<ProductOrder>(product);
                }
                await _context.SaveChangesAsync();
            }

            return await _context.ProductOrders
                .Where(po => po.OrderId == orderId)
                .ToListAsync();
        }
    }
}
