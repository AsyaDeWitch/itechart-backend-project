using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRatingRepository
    {
        public Task<ProductRating> CreateAsync(ProductRating productRating);
        public Task<ProductRating> UpdateAsync(ProductRating productRating);
        public Task DeleteAsync(ProductRating productRating);
    }
}
