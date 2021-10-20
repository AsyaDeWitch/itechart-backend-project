using BLL.ViewModels;
using RIL.Models;

namespace BLL.Interfaces
{
    public interface IProductRatingConverter
    {
        public ProductRating ConvertToProductRating(ProductRatingViewModel productRating);
        public ProductRatingViewModel ConvertToProductRatingViewModel(ProductRating productRating);
    }
}
