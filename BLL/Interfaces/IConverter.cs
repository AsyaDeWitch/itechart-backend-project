namespace BLL.Interfaces
{
    public interface IConverter
    {
        public IAddressConverter Address{ get; }
        public IOrderConverter Order{ get; }
        public IProductRatingConverter ProductRating { get; }
        public IProductOrderConverter ProductOrder { get; }
        public IProductConverter Product { get; }
        public IUserConverter User { get; }
    }
}
