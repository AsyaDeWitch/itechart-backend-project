namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IAddressRepository Addresses { get; }
        public IOrderRepository Orders { get; }
        public IProductOrderRepository ProductOrders { get; }
        public IProductRatingRepository ProductRatings { get; }
        public IProductRepository Products { get; }
    }
}
