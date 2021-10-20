using RIL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<(int, int)>> GetEachPlatformCountAsync();
        public Task<List<Product>> GetProductsByParametersAsync(DateTime term, int limit, double offset, string name);
        public Task<Product> GetByIdAsync(int id);
        public Task<Product> CreateAsync(Product product);
        public Task<Product> UpdateAsync(Product newProduct);
        public Task DeleteByIdAsync(int id);
        public Task UpdateTotalRatingAsync(int id);
        public Task<List<Product>> GetListByAgeAndGenreFilterAsync(int[] genreFilter, int[] ageFilter);
        public Task<bool> IsContainedInDbAsync(int id);
        public Task<int> GetCountByIdAsync(int id);
        public Task UpdateCountAsync(List<ProductOrder> boughtProducts);
    }
}
