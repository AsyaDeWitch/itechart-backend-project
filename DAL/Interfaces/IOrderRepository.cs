using RIL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository
    {
        public Task<bool> BuyAsync(int id);
        public Task<Order> GetByIdAsync(int id);
        public Task<Order> CreateAsync(ExtendedUser user, int totalAmount);
        public Task<List<Order>> GetListByUserIdAsync(int userId);
        public Task<Order> UpdateAsync(Order newOrder);
        public Task<Order> UpdateProductTotalAmountAsync(int id, int totalAmount);
    }
}
