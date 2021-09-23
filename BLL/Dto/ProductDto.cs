using DAL.Data;
using DAL.Repositories;
using RIL.Models;
using System;
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

        public async Task<List<(int, int)>> GetTopPlatformsAsync()
        {
            return await _productRepository.GetEachPlatformCountAsync();
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await _productRepository.GetProductsByNameAsync(name);
        }

        public async Task<List<Product>> GetProductsByParametersWithoutLimitAsync(DateTime term, double offset, string name)
        {
            return await _productRepository.GetProductsByParametersWithoutLimitAsync(term, offset, name);
        }

        public async Task<List<Product>> GetProductsByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            return await _productRepository.GetProductsByParametersAsync(term, limit, offset, name);
        }

        public async Task<Product> GetProductFullInfoAsync(string id)
        {
            return await _productRepository.GetProductFullInfoByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            return await _productRepository.CreateProductAsync(product);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            await _productRepository.DeleteProductByIdAsync(id);
        }

        public async Task UpdateProductTotalRatingAsync(int id)
        {
            await _productRepository.UpdateProductTotalRatingAsync(id);
        }
    }
}
