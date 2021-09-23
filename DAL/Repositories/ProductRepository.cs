using DAL.Data;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<(int, int)>> GetEachPlatformCountAsync()
        {
            var counts = new List<(int, int)>();
            for(int i = 0; i < Enum.GetNames(typeof(Platform)).Length; i++)
            {
                int temp = await _context.Products
                    .Where(p => p.IsDeleted == false)
                    .CountAsync(p => p.Platform == i);
                counts.Add((i, temp));
            }
            return counts;
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            var products = await _context.Products
                .Where(p => p.IsDeleted == false)
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByParametersWithoutLimitAsync(DateTime term, double offset, string name)
        {
            var products = await _context.Products
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .Where(p => p.IsDeleted == false && p.DateCreated >= term && p.TotalRating >= offset)
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            var products = await _context.Products
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .Where(p => p.IsDeleted == false && p.DateCreated >= term && p.TotalRating >= offset)
                .Take(limit)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductFullInfoByIdAsync(string id)
        {
            var product = await _context.Products
                .Where(p => p.Id == int.Parse(id))
                .FirstOrDefaultAsync();

            return product;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.AddAsync<Product>(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(Product newProduct)
        {
            var oldProduct = _context.Products
                .Where(p => p.Id == newProduct.Id)
                .FirstOrDefault();

            if (oldProduct != null)
            {
                if (newProduct.Background != null)
                {
                    oldProduct.Background = newProduct.Background;
                }
                oldProduct.Count = newProduct.Count;
                oldProduct.DateCreated = newProduct.DateCreated;
                oldProduct.Genre = newProduct.Genre;
                if (newProduct.Logo != null)
                {
                    oldProduct.Logo = newProduct.Logo;
                }
                oldProduct.Name = newProduct.Name;
                oldProduct.Platform = newProduct.Platform;
                oldProduct.Price = newProduct.Price;
                oldProduct.Rating = newProduct.Rating;
                oldProduct.TotalRating = newProduct.TotalRating;
            }
            _context.Update<Product>(oldProduct);
            await _context.SaveChangesAsync();

            return oldProduct;
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            var product = _context.Products
                .Where(p => p.Id == int.Parse(id))
                .FirstOrDefault();

            if(product != null)
            {
                product.IsDeleted = true;
                _context.Update<Product>(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductTotalRatingAsync(int id)
        {
            var product = _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefault();
            var productRatings = product.Ratings;

            double sum = 0.0;
            foreach(var productRating in productRatings)
            {
                sum += productRating.Rating;
            }
            product.TotalRating = sum / productRatings.Count;

            _context.Update<Product>(product);
            await _context.SaveChangesAsync();
        }
    }
}
