using DAL.Data;
using DAL.Repositories;
using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ProductOrderDto
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductOrderRepository _productOrderRepository;

        public ProductOrderDto(ApplicationDbContext context)
        {
            _context = context;
            _productOrderRepository = new ProductOrderRepository(_context);
        }

        public async Task<List<ProductOrder>> GetOrderProductListAsync(int orderId)
        {
            return await _productOrderRepository.GetOrderProductListAsync(orderId);
        }

        public async Task DeleteProductsFromOrderAsync(int orderId, List<ProductOrder> products)
        {
           await _productOrderRepository.DeleteProductsFromOrderAsync(orderId, products);
        }

        public async Task<List<ProductOrder>> AddProductsToOrderAsync(int orderId, List<ProductOrder> products)
        {
            return await _productOrderRepository.AddProductsToOrderAsync(orderId, products);
        }

        public async Task<List<ProductOrder>> UpdateOrderProductListAsync(int orderId, List<ProductOrder> products)
        {
            return await _productOrderRepository.UpdateOrderProductListAsync(orderId, products);
        }
    }
}
