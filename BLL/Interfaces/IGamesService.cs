using BLL.ViewModels;
using RIL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGamesService
    {
        public Task<Dictionary<string, int>> GetTopPlatformsAsync(int quantity);
        public Task<List<ReturnProductViewModel>> SearchGamesByParametersAsync(DateTime term, int limit, double offset, string name);
        public Task<ReturnProductViewModel> GetProductFullInfoAsync(string id);
        public Task<ReturnProductViewModel> CreateProductAsync(ProductViewModel product);
        public Task<ReturnProductViewModel> UpdateProductAsync(ProductViewModel product);
        public Task DeleteProductByIdAsync(string id);
        public Task<ProductRatingViewModel> CreateProductRatingAsync(ProductRatingViewModel productRating);
        public Task<ProductRatingViewModel> UpdateProductRatingAsync(ProductRatingViewModel productRating);
        public Task DeleteProductRatingAsync(ProductRatingViewModel productRating);
        public Task<PaginatedList<ReturnProductViewModel>> GetProductListAsync(int? sortingParameter, int[] genreFilter, int[] ageFilter, int? pageNumber, int? pageSize);
    }
}
