using DAL.Data;
using RIL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductOrderRepository : IProductOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductOrder>> GetProductListByOrderIdAsync(int id)
        {
            return await _context.ProductOrders
                .AsNoTracking()
                .Where(po => po.OrderId == id)
                .ToListAsync();
        }

        public async Task DeleteProductsFromOrderAsync(int orderId, List<ProductOrder> deletedProducts)
        {
            foreach(var deletedProduct in deletedProducts)
            {
                var product = await _context.ProductOrders
                   .Where(po => po.OrderId == orderId && po.ProductId == deletedProduct.ProductId)
                   .FirstOrDefaultAsync();
                if (product != null)
                {
                    _context.Remove(product);
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

        public async Task<List<ProductOrder>> UpdateProductListInOrderAsync(int orderId, List<ProductOrder> newProducts)
        {
            foreach(var newProduct in newProducts)
            {
                var product = await _context.ProductOrders
                    .Where(po => po.OrderId == orderId && po.ProductId == newProduct.ProductId)
                    .FirstOrDefaultAsync();
                if (product != null)
                {
                    product.ProductAmount = newProduct.ProductAmount;
                }
                else
                {
                    await _context.AddAsync(newProduct);
                }
                await _context.SaveChangesAsync(); 
            }

            return await _context.ProductOrders
                .Where(po => po.OrderId == orderId)
                .ToListAsync();
        }
    }
}
