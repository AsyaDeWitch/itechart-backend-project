using DAL.Data;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<(int, int)>> GetEachPlatformCountAsync()
        {
            var counts = new List<(int, int)>();
            for(var i = 0; i < Enum.GetNames(typeof(Platform)).Length; i++)
            {
                var temp = await _context.Products
                    .AsNoTracking()
                    .Where(p => p.IsDeleted == false)
                    .CountAsync(p => p.Platform == i);
                counts.Add((i, temp));
            }
            return counts;
        }

        private async Task<List<Product>> GetListByParametersWithoutLimitAsync(DateTime term, double offset, string name)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .Where(p => p.IsDeleted == false && p.DateCreated >= term && p.TotalRating >= offset)
                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsByParametersAsync(DateTime term, int limit, double offset, string name)
        {
            if (name == null)
            {
                name = "";
            }
            if (limit == 0)
            {
                return await GetListByParametersWithoutLimitAsync(term, offset, name);
            }

            return await _context.Products
                .AsNoTracking()
                .Where(p => EF.Functions.Like(p.Name.ToLower(), "%" + name.ToLower() + "%".ToLower()))
                .Where(p => p.IsDeleted == false && p.DateCreated >= term && p.TotalRating >= offset)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product newProduct)
        {
            var product = await _context.Products
                .Where(p => p.Id == newProduct.Id)
                .FirstOrDefaultAsync();

            if (product != null)
            {
                if (newProduct.Background != null)
                {
                    product.Background = newProduct.Background;
                }
                product.Count = newProduct.Count;
                product.DateCreated = newProduct.DateCreated;
                product.Genre = newProduct.Genre;
                if (newProduct.Logo != null)
                {
                    product.Logo = newProduct.Logo;
                }
                product.Name = newProduct.Name;
                product.Platform = newProduct.Platform;
                product.Price = newProduct.Price;
                product.Rating = newProduct.Rating;
                product.TotalRating = newProduct.TotalRating;
            }
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            if(product != null)
            {
                product.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateTotalRatingAsync(int id)
        {
            var product = await _context.Products
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();

            var productRatings = await _context.ProductRatings.Where(pr => pr.ProductId == id).ToListAsync();
            var sum = productRatings
                .Sum(p => p.Rating);
            product.TotalRating = sum / productRatings.Count;
            await _context.SaveChangesAsync();
        }

        private async Task<List<Product>> GetListAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .Where(p => p.IsDeleted == false)
                .ToListAsync();
        }

        private async Task<List<Product>> GetListByAgeFilterAsync(int[] ageFilter)
        {
            return (await _context.Products
                .AsNoTracking()
                .ToListAsync())
                .Where(p => p.IsDeleted == false)
                .Where(p => Array.Exists(ageFilter, x => x == p.Rating))
                .ToList();
        }

        private async Task<List<Product>> GetListByGenreFilterAsync(int[] genreFilter)
        {
            return (await _context.Products
                .AsNoTracking()
                .ToListAsync())
                .Where(p => p.IsDeleted == false)
                .Where(p => Array.Exists(genreFilter, x => x == p.Genre))
                .ToList();
        }

        public async Task<List<Product>> GetListByAgeAndGenreFilterAsync(int[] genreFilter, int[] ageFilter)
        {
            if (genreFilter.Length != 0 && ageFilter.Length != 0)
            {
                return (await _context.Products
                .AsNoTracking()
                .ToListAsync())
                .Where(p => p.IsDeleted == false)
                .Where(p => Array.Exists(genreFilter, x => x == p.Genre) && Array.Exists(ageFilter, x => x == p.Rating))
                .ToList();
            }
            if (ageFilter.Length != 0)
            {
                return await GetListByAgeFilterAsync(ageFilter);
            }
            if (genreFilter.Length != 0)
            { 
                return await GetListByGenreFilterAsync(genreFilter);
            }
            return await GetListAsync();
        }

        public async Task<bool> IsContainedInDbAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.Id == id);
        }

        public async Task<int> GetCountByIdAsync(int id)
        {
            return (await _context.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync()).Count;
        }

        public async Task UpdateCountAsync(List<ProductOrder> boughtProducts)
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
