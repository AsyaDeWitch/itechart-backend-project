using BLL.Interfaces;

namespace BLL.Converters
{
    public class Converter : IConverter
    {
        private readonly IAddressConverter _addressConverter;
        private readonly IOrderConverter _orderConverter;
        private readonly IProductRatingConverter _productRatingConverter;
        private readonly IProductOrderConverter _productOrderConverter;
        private readonly IProductConverter _productConverter;
        private readonly IUserConverter _userConverter;

        public Converter(IAddressConverter addressConverter, IOrderConverter orderConverter,
            IProductRatingConverter productRatingConverter,
            IProductOrderConverter productOrderConverter, IProductConverter productConverter, IUserConverter userConverter)
        {
            _addressConverter = addressConverter;
            _orderConverter = orderConverter;
            _productRatingConverter = productRatingConverter;
            _productOrderConverter = productOrderConverter;
            _productConverter = productConverter;
            _userConverter = userConverter;
        }

        public IAddressConverter Address
        {
            get
            {
                return _addressConverter;
            }
        }

        public IOrderConverter Order
        {
            get
            {
                return _orderConverter;
            }
        }

        public IProductRatingConverter ProductRating
        {
            get
            {
                return _productRatingConverter;
            }
        }

        public IProductOrderConverter ProductOrder
        {
            get
            {
                return _productOrderConverter;
            }
        }

        public IProductConverter Product
        {
            get
            {
                return _productConverter;
            }
        }

        public IUserConverter User
        {
            get
            {
                return _userConverter;
            }
        }
    }
}
