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
    }
}
