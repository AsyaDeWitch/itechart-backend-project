using DAL.Data;
using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductRating> CreateProductRatingAsync(ProductRating productRating)
        {
            if(await _context.ProductRatings.FindAsync(productRating.ProductId, productRating.UserId) == null)
            {
                await _context.ProductRatings.AddAsync(productRating);
                await _context.SaveChangesAsync();

                return productRating;
            }
            return null;
        }

        public async Task<ProductRating> UpdateProductRatingAsync(ProductRating productRating)
        {
            var oldProductRating = await _context.ProductRatings
                .FindAsync(productRating.ProductId, productRating.UserId);
            if (oldProductRating != null)
            {
                oldProductRating.Rating = productRating.Rating;
                await _context.SaveChangesAsync();

                return oldProductRating;
            }
            return null;
        }

        public async Task DeleteProductRatingAsync(ProductRating productRating)
        {
            var oldProductRating = await _context.ProductRatings
                .FindAsync(productRating.ProductId, productRating.UserId);
            if (oldProductRating != null)
            {
                _context.ProductRatings.Remove(oldProductRating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
