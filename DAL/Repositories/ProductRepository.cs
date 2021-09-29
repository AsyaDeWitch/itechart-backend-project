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
                    .AsNoTracking()
                    .Where(p => p.IsDeleted == false)
                    .CountAsync(p => p.Platform == i);
                counts.Add((i, temp));
            }
            return counts;
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByParametersWithoutLimitAsync(DateTime term, double offset, string name)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .Where(p => p.IsDeleted == false && p.DateCreated >= term && p.TotalRating >= offset)
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .Where(p => p.IsDeleted == false && p.DateCreated >= term && p.TotalRating >= offset)
                .Take(limit)
                .ToListAsync();
            return products;
        }

        public async Task<Product> GetProductFullInfoByIdAsync(string id)
        {
            var product = await _context.Products
                .AsNoTracking()
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
            var oldProduct = await _context.Products
                .Where(p => p.Id == newProduct.Id)
                .FirstOrDefaultAsync();

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
            await _context.SaveChangesAsync();

            return oldProduct;
        }

        public async Task DeleteProductByIdAsync(string id)
        {
            var product = await _context.Products
                .Where(p => p.Id == int.Parse(id))
                .FirstOrDefaultAsync();

            if(product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateProductTotalRatingAsync(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            var productRatings = await _context.ProductRatings.Where(pr => pr.ProductId == id).ToListAsync();

            double sum = 0.0;
            foreach(var productRating in productRatings)
            {
                sum += productRating.Rating;
            }
            product.TotalRating = sum / productRatings.Count;

            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var products = await _context.Products
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetProductsByAgeFilterAsync(int[] ageFilter)
        {
            var products =  (await _context.Products
                .AsNoTracking()
                .ToListAsync())
                .Where(p => p.IsDeleted == false)
                .Where(p => Array.Exists(ageFilter, x => x == p.Rating))
                .ToList();
            return products;
        }

        public async Task<List<Product>> GetProductsByGenreFilterAsync(int[] genreFilter)
        {
            var products =  (await _context.Products
                .AsNoTracking()
                .ToListAsync())
                .Where(p => p.IsDeleted == false)
                .Where(p => Array.Exists(genreFilter, x => x == p.Genre))
                .ToList();
            return products;
        }

        public async Task<List<Product>> GetProductsByAgeAndGenreFilterAsync(int[] genreFilter, int[] ageFilter)
        {
            var products = (await _context.Products
                .AsNoTracking()
                .ToListAsync())
                .Where(p => p.IsDeleted == false)
                .Where(p => Array.Exists(genreFilter, x => x == p.Genre) && Array.Exists(ageFilter, x => x == p.Rating))
                .ToList();
            return products;
        }

        public async Task<bool> IsContainedInDb(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<int> GetProductCount(int id)
        {
            return (await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync()).Count;
        }

        public async Task UpdateProductCountAsync(List<ProductOrder> boughtProducts)
        {
            foreach(var boughtProduct in boughtProducts)
            {
                var product = await _context.Products
                    .Where(p => p.Id == boughtProduct.ProductId)
                    .FirstOrDefaultAsync();
                product.Count -= boughtProduct.ProductAmount;
                await _context.SaveChangesAsync();
            }
        }
    }
}
