using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductOrderRepository
    {
        public Task<List<ProductOrder>> GetProductListByOrderIdAsync(int id);
        public Task DeleteProductsFromOrderAsync(int orderId, List<ProductOrder> products);
        public Task<List<ProductOrder>> AddProductsToOrderAsync(int orderId, List<ProductOrder> products);
        public Task<List<ProductOrder>> UpdateProductListInOrderAsync(int orderId, List<ProductOrder> products);
    }
}
