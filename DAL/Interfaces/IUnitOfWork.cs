using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IAddressRepository Addresses { get; }
        public IOrderRepository Orders { get; }
        public IProductOrderRepository ProductOrders { get; }
        public IProductRatingRepository ProductRatings { get; }
        public IProductRepository Products { get; }

        public Task SaveChangesAsync();
    }
}
