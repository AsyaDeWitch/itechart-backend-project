using BLL.Interfaces;

namespace BLL.Converters
{
    public class Converter : IConverter
    {
        public Converter(IAddressConverter addressConverter, IOrderConverter orderConverter,
            IProductRatingConverter productRatingConverter,
            IProductOrderConverter productOrderConverter, IProductConverter productConverter, IUserConverter userConverter)
        {
            Address = addressConverter;
            Order = orderConverter;
            ProductRating = productRatingConverter;
            ProductOrder = productOrderConverter;
            Product = productConverter;
            ProductRating = productRatingConverter;
            User = userConverter;
        }

        public IAddressConverter Address { get; }

        public IOrderConverter Order { get; }

        public IProductRatingConverter ProductRating { get; }

        public IProductOrderConverter ProductOrder { get; }

        public IProductConverter Product { get; }

        public IUserConverter User { get; }
    }
}
