using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGamesService
    {
        public Task<Dictionary<string, int>> GetTopPlatformsAsync(int quantity);
        public Task<List<ProductViewModel>> SearchGamesByNameAsync(string name);
        public Task<List<ProductViewModel>> SearchGamesByParametersAsync(DateTime term, int limit, double offset, string name);
        public Task<ProductViewModel> GetProductFullInfoAsync(string id);
        public Task<ProductViewModel> CreateProductAsync(ProductViewModel product);
    }
}
