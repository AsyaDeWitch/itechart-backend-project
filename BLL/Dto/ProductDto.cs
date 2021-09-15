using DAL.Data;
using DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ProductDto
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _productRepository;

        public ProductDto(ApplicationDbContext context)
        {
            _context = context;
            _productRepository = new ProductRepository(_context);
        }

        public async Task<List<(int, int)>> GetTopPlatforms()
        {
            return await _productRepository.GetEachPlatformCount();
        }
    }
}
