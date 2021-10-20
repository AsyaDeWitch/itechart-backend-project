using AutoMapper;
using BLL.Interfaces;
using BLL.ViewModels;
using RIL.Models;

namespace BLL.Converters
{
    public class ProductRatingConverter : IProductRatingConverter
    {
        private readonly IMapper _mapper;

        public ProductRatingConverter(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ProductRating ConvertToProductRating(ProductRatingViewModel productRating)
        {
            return _mapper.Map<ProductRating>(productRating);
        }

        public ProductRatingViewModel ConvertToProductRatingViewModel(ProductRating productRating)
        {
            return _mapper.Map<ProductRatingViewModel>(productRating);
        }
    }
}
