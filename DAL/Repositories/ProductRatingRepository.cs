using DAL.Data;
using DAL.Interfaces;
using RIL.Models;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRatingRepository : IProductRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductRating> CreateAsync(ProductRating productRating)
        {
            if(await _context.ProductRatings.FindAsync(productRating.ProductId, productRating.UserId) != null)
            {
                return null;
            }

            await _context.ProductRatings.AddAsync(productRating);
            await _context.SaveChangesAsync();
            return productRating;
        }

        public async Task<ProductRating> UpdateAsync(ProductRating newProductRating)
        {
            var productRating = await _context.ProductRatings
                .FindAsync(newProductRating.ProductId, newProductRating.UserId);
            if (productRating == null)
            {
                return null;
            }

            productRating.Rating = newProductRating.Rating;
            await _context.SaveChangesAsync();
            return productRating;
        }

        public async Task DeleteAsync(ProductRating deletedProductRating)
        {
            var productRating = await _context.ProductRatings
                .FindAsync(deletedProductRating.ProductId, deletedProductRating.UserId);
            if (productRating != null)
            {
                _context.ProductRatings.Remove(productRating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
