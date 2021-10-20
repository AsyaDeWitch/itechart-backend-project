using DAL.Data;
using DAL.Interfaces;
using System;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool _isDisposed;

        public UnitOfWork(ApplicationDbContext context, IAddressRepository addressRepository, IOrderRepository orderRepository, IProductOrderRepository productOrderRepository, 
            IProductRatingRepository productRatingRepository, IProductRepository productRepository, IExtendedUserRepository extendedUserRepository)
        {
            _context = context;
            Addresses = addressRepository;
            Orders = orderRepository;
            ProductOrders = productOrderRepository;
            ProductRatings = productRatingRepository;
            Products = productRepository;
            ExtendedUsers = extendedUserRepository;
            _isDisposed = false;
        }

        public IAddressRepository Addresses { get; }


        public IOrderRepository Orders { get; }

        public IProductOrderRepository ProductOrders { get; }

        public IProductRatingRepository ProductRatings { get; }

        public IProductRepository Products { get; }

        public IExtendedUserRepository ExtendedUsers { get; }

        public virtual void Dispose(bool disposing)
        {
            if(!_isDisposed)
            {
                if(disposing)
                {
                     _context.Dispose();
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
