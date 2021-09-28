using DAL.Data;
using DAL.Repositories;
using RIL.Models;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ProductRatingDto
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRatingRepository _productRatingRepository;

        public ProductRatingDto(ApplicationDbContext context)
        {
            _context = context;
            _productRatingRepository = new ProductRatingRepository(_context);
        }

        public async Task<ProductRating> CreateProductRatingAsync(ProductRating productRating)
        {
            return await _productRatingRepository.CreateProductRatingAsync(productRating);
        }

        public async Task<ProductRating> UpdateProductRatingAsync(ProductRating productRating)
        {
            return await _productRatingRepository.UpdateProductRatingAsync(productRating);
        }

        public async Task DeleteProductRatingAsync(ProductRating productRating)
        {
           await _productRatingRepository.DeleteProductRatingAsync(productRating);
        }
    }
}
