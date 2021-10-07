using DAL.Data;
using DAL.Interfaces;
using System;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductOrderRepository _productOrderRepository;
        private readonly IProductRatingRepository _productRatingRepository;
        private readonly IProductRepository _productRepository;
        private bool _isDisposed;

        public UnitOfWork(ApplicationDbContext context, IAddressRepository addressRepository, IOrderRepository orderRepository,
            IProductOrderRepository productOrderRepository, IProductRatingRepository productRatingRepository, IProductRepository productRepository)
        {
            _context = context;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _productOrderRepository = productOrderRepository;
            _productRatingRepository = productRatingRepository;
            _productRepository = productRepository;
            _isDisposed = false;
        }

        public IAddressRepository Addresses
        {
            get
            {
                return _addressRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                return _orderRepository;
            }
        }

        public IProductOrderRepository ProductOrders
        {
            get
            {
                return _productOrderRepository;
            }
        }

        public IProductRatingRepository ProductRatings
        {
            get
            {
                return _productRatingRepository;
            }
        }

        public IProductRepository Products
        {
            get
            {
                return _productRepository;
            }
        }

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
